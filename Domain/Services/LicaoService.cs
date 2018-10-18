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

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(licaoDTO.IdCase);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            Licao licao = new Licao();
            licao.IdCase = caseDeNegocio.Id;
            licao.CaseDeNegocio = caseDeNegocio;

            licaoDTO.PreencherEntidade(licao);

            Adicionar(licao);

            return licao.Id;
        }

        public void Atualizar(LicaoDTO licaoDTO)
        {
            if (licaoDTO == null || !licaoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            Licao licao = ObterPorId(licaoDTO.Id.Value);

            if (licao == null || licao.IdCase != licaoDTO.IdCase)
                throw new Exception("Lição não encontrada.");

            licaoDTO.PreencherEntidade(licao);

            Atualizar(licao);
        }

        public IEnumerable<LicaoDTO> Listar(int idCaseDeNegocio, int? idLicao = null)
        {
            var response = new List<LicaoDTO>();

            var licoes = _licaoRepository.Listar(idCaseDeNegocio, idLicao);

            foreach (var licao in licoes)
            {
                var licaoDTO = new LicaoDTO(licao);
                response.Add(licaoDTO);
            }

            return null;
        }
    }
}