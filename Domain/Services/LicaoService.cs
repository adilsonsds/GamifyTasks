using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class LicaoService : BaseService<Licao>, ILicaoService
    {
        private readonly ILicaoRepository _licaoRepository;
        private readonly ICaseDeNegocioService _caseDeNegocioService;

        public LicaoService(ILicaoRepository licaoRepository, ICaseDeNegocioService caseDeNegocioService)
            : base(licaoRepository)
        {
            _licaoRepository = licaoRepository;
            _caseDeNegocioService = caseDeNegocioService;
        }

        public int Adicionar(LicaoDTO licaoDTO)
        {
            if (licaoDTO == null || licaoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (licaoDTO.Questoes == null || !licaoDTO.Questoes.Any())
                throw new Exception("É necessário ter pelo menos 1 questão.");

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(licaoDTO.IdCase);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            Licao licao = new Licao();
            licao.IdCase = caseDeNegocio.Id;
            licao.CaseDeNegocio = caseDeNegocio;

            licaoDTO.PreencherEntidade(licao);
            AtualizarListaDeQuestoes(licao, licaoDTO.Questoes);

            Adicionar(licao);

            return licao.Id;
        }

        public void Atualizar(LicaoDTO licaoDTO)
        {
            if (licaoDTO == null || !licaoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (licaoDTO.Questoes == null || !licaoDTO.Questoes.Any())
                throw new Exception("É necessário ter pelo menos 1 questão.");

            Licao licao = ObterPorId(licaoDTO.Id.Value);

            if (licao == null || licao.IdCase != licaoDTO.IdCase)
                throw new Exception("Lição não encontrada.");

            licaoDTO.PreencherEntidade(licao);
            AtualizarListaDeQuestoes(licao, licaoDTO.Questoes);

            Atualizar(licao);
        }

        public IEnumerable<LicaoDTO> Listar(int idCaseDeNegocio, int? idLicao = null)
        {
            List<LicaoDTO> response = new List<LicaoDTO>();

            var licoes = _licaoRepository.Listar(idCaseDeNegocio, idLicao);

            foreach (var licao in licoes)
            {
                LicaoDTO licaoDTO = new LicaoDTO(licao);
                licaoDTO.PermiteEditar = UsuarioLogadoPodeEditarLicao();
                response.Add(licaoDTO);
            }

            return response;
        }

        public LicaoDTO Obter(int idCaseDeNegocio, int idLicao)
        {
            var licao = ObterPorId(idLicao);

            if (licao == null || licao.IdCase != idCaseDeNegocio)
                throw new Exception("Lição não encontrada.");

            LicaoDTO licaoDTO = new LicaoDTO(licao);
            licaoDTO.PermiteEditar = UsuarioLogadoPodeEditarLicao();

            return licaoDTO;
        }

        #region Métodos privados
        private bool UsuarioLogadoPodeEditarLicao()
        {
            return true;
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

            List<Questao> questoesRemovidas = licao.Questoes.Where(qe => !questoesParaSalvar.Any(qs => qs.Id == qe.Id)).ToList();
            foreach (var questaoRemovida in questoesRemovidas)
            {
                licao.Questoes.Remove(questaoRemovida);
            }
        }
        #endregion
    }
}