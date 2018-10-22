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

        public CaseDeNegocioService(ICaseDeNegocioRepository caseDeNegocioRepository)
            : base(caseDeNegocioRepository)
        {
            _caseDeNegocioRepository = caseDeNegocioRepository;
        }

        public int Adicionar(CaseDTO caseDTO, Usuario usuarioLogado)
        {
            if (caseDTO == null || caseDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (usuarioLogado == null)
                throw new Exception("É necessário um usuário autenticado para realizar esta ação.");

            CaseDeNegocio caseDeNegocio = new CaseDeNegocio();
            caseDeNegocio.IdProfessor = usuarioLogado.Id;
            caseDeNegocio.Professor = usuarioLogado;
            
            caseDTO.PreencherEntidade(caseDeNegocio);

            Adicionar(caseDeNegocio);

            return caseDeNegocio.Id;
        }

        public void Atualizar(CaseDTO caseDTO, Usuario usuarioLogado)
        {
            if (caseDTO == null || !caseDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            if (usuarioLogado == null)
                throw new Exception("É necessário um usuário autenticado para realizar esta ação.");

            CaseDeNegocio caseDeNegocio = ObterPorId(caseDTO.Id.Value);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            if (caseDeNegocio.Professor != usuarioLogado)
                throw new Exception("Somente o professor pode atualizar os dados.");

            caseDTO.PreencherEntidade(caseDeNegocio);

            Atualizar(caseDeNegocio);
        }

        public IEnumerable<CaseDTO> Listar(Usuario usuarioLogado, int? idCaseDeNegocio = null)
        {
            List<CaseDTO> response = new List<CaseDTO>();

            var casesDeNegocios = _caseDeNegocioRepository.ListarPorProfessor(usuarioLogado.Id);

            foreach (var caseDeNegocio in casesDeNegocios)
            {
                CaseDTO caseDTO = new CaseDTO(caseDeNegocio);
                response.Add(caseDTO);
            }

            return response;
        }

        public CaseDTO ObterPorId(int idCaseDeNegocio, Usuario usuarioLogado)
        {
            var caseDeNegocio = ObterPorId(idCaseDeNegocio);

            var response = new CaseDTO(caseDeNegocio);

            if (usuarioLogado != null)
            {
                response.PermiteSeInscrever = true;
            }

            return response;

        }
    }
}