using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Request;

namespace Domain.Services
{
    public class GeracaoDeEntregaDeLicaoService : BaseService, IGeracaoDeEntregaDeLicaoService
    {
        private readonly ILicaoRepository _licaoRepository;
        private readonly IConsultaDeAlunosService _consultaDeAlunosService;
        private readonly IEntregaDeLicaoService _entregaDeLicaoService;
        private readonly IConsultaEntregaDeLicaoService _consultaEntregaService;

        public GeracaoDeEntregaDeLicaoService(ILicaoRepository licaoRepository, IConsultaDeAlunosService consultaDeAlunosService,
            IEntregaDeLicaoService entregaDeLicaoService, IConsultaEntregaDeLicaoService consultaEntregaService)
        {
            _consultaDeAlunosService = consultaDeAlunosService;
            _licaoRepository = licaoRepository;
            _entregaDeLicaoService = entregaDeLicaoService;
            _consultaEntregaService = consultaEntregaService;
        }

        public int Gerar(int idCase, int idLicao, Usuario usuarioLogado)
        {
            AlunoDoCase alunoDoCase = _consultaDeAlunosService.ObterAlunoInscritoNoCase(usuarioLogado.Id, idCase);

            if (alunoDoCase == null)
                throw new Exception("Usuário não está inscrito no case de negócio.");

            Licao licao = _licaoRepository.GetById(idLicao);

            if (licao == null || licao.IdCase != idCase)
                throw new Exception("Solicitação inválida.");

            if (_consultaEntregaService.AlunoJaComecouAFazerALicao(alunoDoCase.Id, licao.Id))
                throw new Exception("O registro de entrega desta lição já foi criado.");

            EntregaDeLicao entrega = new EntregaDeLicao(licao);

            AdicionarResponsaveisPelaLicao(entrega, alunoDoCase);

            _entregaDeLicaoService.Manter(entrega);

            return entrega.Id;
        }

        public void Salvar(SalvarEntregaDeLicaoRequest request, Usuario usuarioLogado)
        {
            var entrega = _entregaDeLicaoService.ObterPorId(request.IdEntregaDeLicao);

            if (entrega == null)
                throw new Exception("Entrega de lição não encontrada.");

            if (!_consultaEntregaService.UsuarioEhResponsavelPelaEntregaDeLicao(entrega.Id, usuarioLogado.Id))
                throw new Exception("Entrega de lição não encontrada.");

            if (!TodasAsQuestoesRespondidasFazemParteDaMesmaLicao(entrega, request.Questoes))
                throw new Exception("Solicitação possui questões que não fazem parte da lição.");

            AlterarStatus(entrega, request.Status);

            foreach (var questao in request.Questoes)
            {
                Resposta resposta = entrega.Respostas.FirstOrDefault(r => r.IdQuestao == questao.Id);
                if (resposta == null)
                {
                    resposta = new Resposta();
                    resposta.IdEntregaDeLicao = entrega.Id;
                    resposta.IdQuestao = questao.Id;
                    entrega.Respostas.Add(resposta);
                }

                resposta.Conteudo = questao.Resposta;
            }


            _entregaDeLicaoService.Manter(entrega);
        }

        private bool TodasAsQuestoesRespondidasFazemParteDaMesmaLicao(EntregaDeLicao entrega, IList<QuestaoRequest> questoesRequest)
        {
            return questoesRequest.All(qr => entrega.Licao.Questoes.Any(q => q.Id == qr.Id));
        }

        private void AlterarStatus(EntregaDeLicao entrega, EntregaDeLicaoStatusEnum novoStatus)
        {
            EntregaDeLicaoStatusEnum statusAnterior = entrega.Status;

            switch (novoStatus)
            {
                case EntregaDeLicaoStatusEnum.NaoEntregue:
                    if (statusAnterior == EntregaDeLicaoStatusEnum.Entregue)
                        throw new Exception("Não é possível alterar o status de uma tarefa entregue.");
                    break;
                case EntregaDeLicaoStatusEnum.Entregue:
                    if (statusAnterior == EntregaDeLicaoStatusEnum.Entregue)
                        throw new Exception("Esta tarefa já foi entregue.");
                    break;
                default:
                    throw new Exception("Status não implementado.");
            }

            entrega.Status = novoStatus;
        }

        private void AdicionarResponsaveisPelaLicao(EntregaDeLicao entrega, AlunoDoCase alunoDoCase)
        {
            entrega.Responsaveis.Add(new ResponsavelPelaLicao(entrega, alunoDoCase));

            if (entrega.Licao.FormaDeEntrega != FormaDeEntregaDaLicaoEnum.SomenteIndividual)
            {
                int? idGrupo = _consultaDeAlunosService.ObterIdGrupoOndeUsuarioEstejaParticipando(alunoDoCase.IdUsuario, alunoDoCase.IdCaseDeNegocio);

                if (idGrupo == null && entrega.Licao.FormaDeEntrega == FormaDeEntregaDaLicaoEnum.SomenteEmGrupo)
                    throw new Exception("Esta lição requer que o aluno esteja participando de um grupo.");

                if (idGrupo.HasValue)
                {
                    entrega.IdGrupo = idGrupo.Value;
                    var outrosMembrosDoGrupo = _consultaDeAlunosService.ListarOutrosAlunosQueSejamMembrosNoGrupo(entrega.IdGrupo.Value, alunoDoCase.Id);

                    foreach (var alunoMembro in outrosMembrosDoGrupo)
                        entrega.Responsaveis.Add(new ResponsavelPelaLicao(entrega, alunoMembro));
                }
            }
        }
    }
}
