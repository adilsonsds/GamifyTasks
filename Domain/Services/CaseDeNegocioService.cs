using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class CaseDeNegocioService : BaseService<CaseDeNegocio>, ICaseDeNegocioService
    {
        private readonly ICaseDeNegocioRepository _caseDeNegocioRepository;
        private readonly IUsuarioService _usuarioService;

        public CaseDeNegocioService(ICaseDeNegocioRepository caseDeNegocioRepository, IUsuarioService usuarioService)
            : base(caseDeNegocioRepository)
        {
            _caseDeNegocioRepository = caseDeNegocioRepository;
            _usuarioService = usuarioService;
        }

        public int Adicionar(CaseDTO caseDTO)
        {
            if (caseDTO == null || caseDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            CaseDeNegocio caseDeNegocio = new CaseDeNegocio();
            caseDTO.PreencherEntidade(caseDeNegocio);

            Adicionar(caseDeNegocio);

            return caseDeNegocio.Id;
        }

        public void Atualizar(CaseDTO caseDTO)
        {
            if (caseDTO == null || !caseDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            CaseDeNegocio caseDeNegocio = ObterPorId(caseDTO.Id.Value);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            caseDTO.PreencherEntidade(caseDeNegocio);

            Atualizar(caseDeNegocio);
        }

        public IEnumerable<CaseDTO> Listar(int? idCaseDeNegocio = null)
        {
            List<CaseDTO> response = new List<CaseDTO>();

            var casesDeNegocios = _caseDeNegocioRepository.ListarPorProfessor(1);

            foreach (var caseDeNegocio in casesDeNegocios)
            {
                CaseDTO caseDTO = new CaseDTO(caseDeNegocio);
                response.Add(caseDTO);
            }

            return response;
        }
    }
}