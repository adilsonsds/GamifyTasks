using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Request;

namespace Domain.Services
{
    public class EntregaDeTrofeuService : BaseService<EntregaDeTrofeu>, IEntregaDeTrofeuService
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly ITrofeuRepository _trofeuRepository;
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;
        private readonly IRespostaRepository _respostaRepository;
        private readonly IResponsavelPelaLicaoRepository _responsavelPelaLicaoRepository;
        private readonly IAlunoDoCaseRepository _alunoDoCaseRepository;

        public EntregaDeTrofeuService(IEntregaDeTrofeuRepository repository, ICaseDeNegocioService caseDeNegocioService,
            ITrofeuRepository trofeuRepository, IEntregaDeLicaoRepository entregaDeLicaoRepository, IRespostaRepository respostaRepository,
            IResponsavelPelaLicaoRepository responsavelPelaLicaoRepository, IAlunoDoCaseRepository alunoDoCaseRepository)
            : base(repository)
        {
            _caseDeNegocioService = caseDeNegocioService;
            _trofeuRepository = trofeuRepository;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
            _respostaRepository = respostaRepository;
            _responsavelPelaLicaoRepository = responsavelPelaLicaoRepository;
            _alunoDoCaseRepository = alunoDoCaseRepository;
        }

        public void Atribuir(AtribuirTrofeuRequest request, Usuario usuario)
        {
            Trofeu trofeu = _trofeuRepository.GetById(request.IdTrofeu);
            if (trofeu == null)
                throw new Exception("Troféu não localizado.");

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(trofeu.IdCase);

            if (!_caseDeNegocioService.UsuarioEstaAssociadoAoCaseDeNegocioComoProfessor(usuario, caseDeNegocio))
                throw new Exception("Usuário não possui permissão para esta solicitação.");

            EntregaDeLicao entregaDeLicao = _entregaDeLicaoRepository.GetById(request.IdEntregaDeLicao);
            if (entregaDeLicao == null)
                throw new Exception("Lição não localizada.");
            else if (entregaDeLicao.Licao.IdCase != caseDeNegocio.Id)
                throw new Exception("Lição não pode ser associada à este troféu.");

            Resposta resposta = null;
            if (request.IdResposta != null && request.IdResposta > 0)
            {
                resposta = _respostaRepository.GetById(request.IdResposta.Value);
                if (resposta == null)
                    throw new Exception("Questão não localizada.");
                else if (resposta.IdEntregaDeLicao != entregaDeLicao.Id)
                    throw new Exception("Resposta não associada à lição.");
            }

            EntregaDeTrofeu entregaDeTrofeu = new EntregaDeTrofeu(trofeu, entregaDeLicao, resposta);

            Adicionar(entregaDeTrofeu);
        }

        public IList<EntregaDeTrofeuDTO> Listar(FiltroTrofeusRequest filtro)
        {
            if (!filtro.IdEntregaDeLicao.HasValue && !filtro.IdGrupo.HasValue && !filtro.IdUsuario.HasValue)
                throw new Exception("Solicitação inválida.");

            IQueryable<EntregaDeTrofeu> queryTrofeusEntregues = _repository.Queryable();

            if (filtro.IdEntregaDeLicao.HasValue)
            {
                queryTrofeusEntregues = from et in queryTrofeusEntregues
                                        join el in _entregaDeLicaoRepository.Queryable() on et.IdEntregaDeLicao equals el.Id
                                        where el.Id == filtro.IdEntregaDeLicao.Value
                                        select et;
            }
            else if (filtro.IdGrupo.HasValue)
            {
                queryTrofeusEntregues = from et in queryTrofeusEntregues
                                        join el in _entregaDeLicaoRepository.Queryable() on et.IdEntregaDeLicao equals el.Id
                                        where el.IdGrupo == filtro.IdGrupo.Value
                                        select et;
            }
            else if (filtro.IdUsuario.HasValue)
            {
                queryTrofeusEntregues = from et in queryTrofeusEntregues
                                        join r in _responsavelPelaLicaoRepository.Queryable() on et.IdEntregaDeLicao equals r.IdEntregaDeLicao
                                        join a in _alunoDoCaseRepository.Queryable() on r.IdAluno equals a.Id
                                        where a.IdUsuario == filtro.IdUsuario.Value
                                        select et;
            }

            var trofeus = (from et in queryTrofeusEntregues
                           join t in _trofeuRepository.Queryable() on et.IdTrofeu equals t.Id
                           select new EntregaDeTrofeuDTO
                           {
                               IdEntrega = et.Id,
                               IdTrofeu = et.IdTrofeu,
                               NomeTrofeu = t.Nome,
                               PontosMovimentados = t.Pontos
                           }).ToList();

            return trofeus;
        }

        public IList<KeyValuePair<int, EntregaDeTrofeuDTO>> ListarTrofeusRecebidosPelaLicaoEntregue(IQueryable<EntregaDeLicao> entregasDeLicoesFiltradas)
        {
            var entregasDeTrofeus = from el in entregasDeLicoesFiltradas
                                    join et in _repository.Queryable() on el.Id equals et.IdEntregaDeLicao
                                    join t in _trofeuRepository.Queryable() on et.IdTrofeu equals t.Id
                                    select new KeyValuePair<int, EntregaDeTrofeuDTO>(el.Id, new EntregaDeTrofeuDTO
                                    {
                                        IdTrofeu = et.IdTrofeu,
                                        NomeTrofeu = t.Nome,
                                        PontosMovimentados = t.Pontos
                                    });

            return entregasDeTrofeus.ToList();
        }
    }
}