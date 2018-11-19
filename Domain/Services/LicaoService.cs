using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class LicaoService : BaseService<Licao>, ILicaoService
    {
        private readonly ILicaoRepository _licaoRepository;
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly IConsultaEntregaDeLicaoService _consultaEntregaDeLicaoService;
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;
        private readonly IEntregaDeTrofeuRepository _entregaDeTrofeuRepository;
        private readonly ITrofeuRepository _trofeuRepository;
        private readonly IRespostaRepository _respostaRepository;

        public LicaoService(ILicaoRepository licaoRepository, ICaseDeNegocioService caseDeNegocioService,
            IConsultaEntregaDeLicaoService consultaEntregaDeLicaoService, IEntregaDeLicaoRepository entregaDeLicaoRepository,
            IEntregaDeTrofeuRepository entregaDeTrofeuRepository, ITrofeuRepository trofeuRepository, IRespostaRepository respostaRepository)
                : base(licaoRepository)
        {
            _licaoRepository = licaoRepository;
            _caseDeNegocioService = caseDeNegocioService;
            _consultaEntregaDeLicaoService = consultaEntregaDeLicaoService;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
            _entregaDeTrofeuRepository = entregaDeTrofeuRepository;
            _trofeuRepository = trofeuRepository;
            _respostaRepository = respostaRepository;
        }

        public int Adicionar(LicaoDTO licaoDTO, Usuario usuarioLogado)
        {
            if (licaoDTO == null || licaoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (licaoDTO.Questoes == null || !licaoDTO.Questoes.Any())
                throw new Exception("É necessário ter pelo menos 1 questão.");

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(licaoDTO.IdCase);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            if (!_caseDeNegocioService.PermiteUsuarioEditarCaseDeNegocio(usuarioLogado, caseDeNegocio))
                throw new Exception("Usuário não possui permissão para esta solicitação.");

            Licao licao = new Licao();
            licao.IdCase = caseDeNegocio.Id;
            licao.CaseDeNegocio = caseDeNegocio;

            licaoDTO.PreencherEntidade(licao);
            AtualizarListaDeQuestoes(licao, licaoDTO.Questoes);

            Adicionar(licao);

            return licao.Id;
        }

        public void Atualizar(LicaoDTO licaoDTO, Usuario usuarioLogado)
        {
            if (licaoDTO == null || !licaoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (licaoDTO.Questoes == null || !licaoDTO.Questoes.Any())
                throw new Exception("É necessário ter pelo menos 1 questão.");

            Licao licao = ObterPorId(licaoDTO.Id.Value);

            if (licao == null || licao.IdCase != licaoDTO.IdCase)
                throw new Exception("Lição não encontrada.");

            if (!_caseDeNegocioService.PermiteUsuarioEditarCaseDeNegocio(usuarioLogado, licao.CaseDeNegocio))
                throw new Exception("Usuário não possui permissão para esta solicitação.");

            licaoDTO.PreencherEntidade(licao);
            AtualizarListaDeQuestoes(licao, licaoDTO.Questoes);

            Atualizar(licao);
        }

        public IEnumerable<LicaoDTO> Listar(int idCaseDeNegocio, Usuario usuarioLogado)
        {
            List<LicaoDTO> response = new List<LicaoDTO>();

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(idCaseDeNegocio);

            bool permiteEditar = _caseDeNegocioService.PermiteUsuarioEditarCaseDeNegocio(usuarioLogado, caseDeNegocio);
            bool ehAlunoInscrito = _caseDeNegocioService.UsuarioEstaInscritoNoCaseDeNegocio(usuarioLogado, caseDeNegocio);
            var licoesIniciadas = _consultaEntregaDeLicaoService.ListarEntregasIniciadasPeloUsuarioNoCaseDeNegocio(caseDeNegocio.Id, usuarioLogado.Id);

            // TODO:erro ao obter lições mapeadas no case de negócio. Necessário verificar mapeamento do EF Core. Pode ser algo com o proxy.
            var licoes = _licaoRepository.ListarPorCaseDeNegocio(caseDeNegocio.Id);

            foreach (var licao in licoes)
            {
                LicaoDTO licaoDTO = new LicaoDTO(licao);
                PreencherDadosDePermissaoEEntrega(licaoDTO, permiteEditar, ehAlunoInscrito, licoesIniciadas);
                response.Add(licaoDTO);
            }

            return response;
        }

        public LicaoDTO Obter(int idCaseDeNegocio, int idLicao, Usuario usuarioLogado)
        {
            var licao = ObterPorId(idLicao);

            if (licao == null || licao.IdCase != idCaseDeNegocio)
                throw new Exception("Lição não encontrada.");

            LicaoDTO licaoDTO = new LicaoDTO(licao);

            bool permiteEditar = _caseDeNegocioService.PermiteUsuarioEditarCaseDeNegocio(usuarioLogado, licao.CaseDeNegocio);
            bool ehAlunoInscrito = _caseDeNegocioService.UsuarioEstaInscritoNoCaseDeNegocio(usuarioLogado, licao.CaseDeNegocio);

            var licoesIniciadas = _consultaEntregaDeLicaoService.ListarEntregasIniciadasPeloUsuarioNoCaseDeNegocio(licao.IdCase, usuarioLogado.Id);
            PreencherDadosDePermissaoEEntrega(licaoDTO, permiteEditar, ehAlunoInscrito, licoesIniciadas);

            return licaoDTO;
        }

        public LicaoDTO ObterEntrega(int IdEntregaDeLicao, Usuario usuarioLogado)
        {
            var entrega = _entregaDeLicaoRepository.GetById(IdEntregaDeLicao);

            if (entrega == null)
                throw new Exception("Registro de entrega não localizado.");

            var responsaveis = _consultaEntregaDeLicaoService.ListarResponsaveisPelaEntregaDeLicao(entrega.Id);

            if (!_consultaEntregaDeLicaoService.PermiteVisualizarLicao(usuarioLogado, responsaveis))
                throw new Exception("Usuário não possui permissão para visualizar esta lição.");

            var response = new LicaoDTO(entrega);
            response.Responsaveis = responsaveis;

            bool permiteEditar = _caseDeNegocioService.PermiteUsuarioEditarCaseDeNegocio(usuarioLogado, entrega.Licao.CaseDeNegocio);
            bool ehAlunoInscrito = _caseDeNegocioService.UsuarioEstaInscritoNoCaseDeNegocio(usuarioLogado, entrega.Licao.CaseDeNegocio);

            var licoesIniciadas = _consultaEntregaDeLicaoService.ListarEntregasIniciadasPeloUsuarioNoCaseDeNegocio(entrega.Licao.IdCase, usuarioLogado.Id);
            PreencherDadosDePermissaoEEntrega(response, permiteEditar, ehAlunoInscrito, licoesIniciadas);

            if (entrega.Status == EntregaDeLicaoStatusEnum.Entregue)
                AplicarTrofeusRecebidos(response);

            return response;
        }

        #region Métodos privados
        private void AplicarTrofeusRecebidos(LicaoDTO response)
        {
            var entregaDeTrofeus = (from et in _entregaDeTrofeuRepository.Queryable()
                                    join t in _trofeuRepository.Queryable() on et.IdTrofeu equals t.Id
                                    join r in _respostaRepository.Queryable() on et.IdResposta equals r.Id
                                    select new KeyValuePair<int, EntregaDeTrofeuDTO>(r.IdQuestao, new EntregaDeTrofeuDTO
                                    {
                                        IdEntrega = et.Id,
                                        IdTrofeu = t.Id,
                                        NomeTrofeu = t.Nome,
                                        PontosMovimentados = t.Pontos
                                    })).ToList();

            foreach (var questao in response.Questoes)
            {
                questao.Trofeus = entregaDeTrofeus.Where(t => t.Key == questao.Id).Select(t => t.Value).ToList();
            }
        }

        private void AtualizarListaDeQuestoes(Licao licao, IEnumerable<QuestaoDTO> questoesParaSalvar)
        {
            foreach (var questaoDTO in questoesParaSalvar)
            {
                Questao questao = licao.Questoes.FirstOrDefault(q => q.Id == questaoDTO.Id);
                if (questao == null)
                {
                    questao = new Questao
                    {
                        IdLicao = licao.Id,
                        // Licao = licao
                    };

                    licao.Questoes.Add(questao);
                }

                questaoDTO.PreencherEntidade(questao);
            }

            List<Questao> questoesRemovidas = licao.Questoes.Where(qe => qe.Id > 0 && !questoesParaSalvar.Any(qs => qs.Id == qe.Id)).ToList();
            foreach (var questaoRemovida in questoesRemovidas)
            {
                licao.Questoes.Remove(questaoRemovida);
            }
        }

        private void PreencherDadosDePermissaoEEntrega(LicaoDTO licaoDTO, bool permiteEditar, bool ehAlunoInscrito, IEnumerable<EntregaDeLicaoIniciadaDTO> licoesIniciadas)
        {
            bool dataLiberacaoJaPassou = licaoDTO.DataLiberacao == null || licaoDTO.DataLiberacao < DateTime.Now;

            if (permiteEditar)
            {
                licaoDTO.EhProfessor = true;
                licaoDTO.EhAluno = false;
                licaoDTO.PermiteVisualizar = true;
                licaoDTO.PermiteEditar = true;
                licaoDTO.PermiteAvaliar = dataLiberacaoJaPassou;
                licaoDTO.PermiteRealizar = false;
                licaoDTO.IdEntregaDeLicao = null;
                licaoDTO.PermiteEntregar = false;
            }
            else if (ehAlunoInscrito)
            {
                licaoDTO.EhProfessor = false;
                licaoDTO.EhAluno = true;
                licaoDTO.PermiteVisualizar = dataLiberacaoJaPassou;
                licaoDTO.PermiteEditar = false;
                licaoDTO.PermiteAvaliar = false;

                EntregaDeLicaoIniciadaDTO entregaIniciada = licoesIniciadas.FirstOrDefault(l => l.IdLicao == licaoDTO.Id);

                if (entregaIniciada != null)
                {
                    licaoDTO.IdEntregaDeLicao = entregaIniciada.IdEntregaDeLicao;

                    bool foiEntregue = entregaIniciada.Status == EntregaDeLicaoStatusEnum.Entregue;
                    licaoDTO.Entregue = foiEntregue;
                    licaoDTO.DataHoraEntrega = entregaIniciada.DataHoraEntrega;
                    licaoDTO.PermiteEntregar = !foiEntregue;
                    licaoDTO.PermiteRealizar = !foiEntregue;
                }
                else
                {
                    licaoDTO.Entregue = false;
                    licaoDTO.DataHoraEntrega = null; ;
                    licaoDTO.PermiteRealizar = dataLiberacaoJaPassou;
                    licaoDTO.IdEntregaDeLicao = null;
                    licaoDTO.PermiteEntregar = false;
                }
            }
            else
            {
                licaoDTO.EhProfessor = false;
                licaoDTO.EhAluno = false;
                licaoDTO.PermiteVisualizar = false;
                licaoDTO.PermiteEditar = false;
                licaoDTO.PermiteAvaliar = false;
                licaoDTO.PermiteRealizar = false;
                licaoDTO.IdEntregaDeLicao = null;
                licaoDTO.PermiteEntregar = false;
            }
        }

        #endregion
    }
}