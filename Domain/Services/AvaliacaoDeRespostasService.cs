using System;
using System.Linq;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Request;
using Domain.Enums;

namespace Domain.Services
{
    public class AvaliacaoDeRespostasService : BaseService, IAvaliacaoDeRespostasService
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly ILicaoRepository _licaoRepository;
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;
        private readonly IRespostaRepository _respostaRepository;
        private readonly ITrofeuRepository _trofeuRepository;

        public AvaliacaoDeRespostasService(ICaseDeNegocioService caseDeNegocioService, ILicaoRepository licaoRepository,
            IEntregaDeLicaoRepository entregaDeLicaoRepository, IRespostaRepository respostaRepository, ITrofeuRepository trofeuRepository)
        {
            _caseDeNegocioService = caseDeNegocioService;
            _licaoRepository = licaoRepository;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
            _respostaRepository = respostaRepository;
            _trofeuRepository = trofeuRepository;
        }

        public AvaliarRespostasDTO ObterDadosDePreparacaoParaAvaliarRespostas(int idLicao, Usuario usuarioLogado)
        {
            Licao licao = _licaoRepository.GetById(idLicao);

            if (licao == null)
                throw new Exception("Lição não encontrada.");

            bool ehProfessor = _caseDeNegocioService.UsuarioEstaAssociadoAoCaseDeNegocioComoProfessor(usuarioLogado, licao.CaseDeNegocio);

            if (!ehProfessor)
                throw new Exception("Apenas professores têm permissão para avaliar as lições entregues.");

            var trofeus = _trofeuRepository.Listar(licao.IdCase);

            return new AvaliarRespostasDTO(licao, trofeus);
        }

        public IList<QuestaoParaAvaliarDTO> ListarQuestoesParaAvaliar(FiltrarQuestoesRequest filtro)
        {
            var queryRespostas = _respostaRepository.Queryable().Where(r => r.IdQuestao == filtro.IdQuestao);

            if (filtro.RemoverQuestoesJaAvaliadas)
                queryRespostas = queryRespostas.Where(r => r.PontosGanhos == null);

            var questoes = (from r in queryRespostas
                            join e in _entregaDeLicaoRepository.Queryable() on r.IdEntregaDeLicao equals e.Id
                            where e.Status == EntregaDeLicaoStatusEnum.Entregue
                            orderby e.DataHoraEntrega ascending
                            select new QuestaoParaAvaliarDTO
                            {
                                IdEntregaDeLicao = e.Id,
                                IdResposta = r.Id,
                                Resposta = r.Conteudo,
                                IdQuestao = r.IdQuestao,
                                DataHoraEntrega = e.DataHoraEntrega,
                                IdGrupo = e.IdGrupo,
                                PontosRecebidos = r.PontosGanhos
                            }).ToList();

            return questoes;
        }

        public void AtribuirPontosParaResposta(AtribuirNotaRequest request, Usuario usuarioLogado)
        {
            Resposta resposta = _respostaRepository.GetById(request.IdResposta);

            if (resposta == null)
                throw new Exception("Resposta não encontrada.");

            int idCase = (from r in _respostaRepository.Queryable()
                          join e in _entregaDeLicaoRepository.Queryable() on r.IdEntregaDeLicao equals e.Id
                          join l in _licaoRepository.Queryable() on e.IdLicao equals l.Id
                          where r.Id == request.IdResposta
                          select l.IdCase).FirstOrDefault();

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(idCase);

            if (!_caseDeNegocioService.UsuarioEstaAssociadoAoCaseDeNegocioComoProfessor(usuarioLogado, caseDeNegocio))
                throw new Exception("Usuário não possui permissão para atribuir notas.");

            resposta.PontosGanhos = request.Pontos;

            _respostaRepository.Update(resposta);
        }
    }
}