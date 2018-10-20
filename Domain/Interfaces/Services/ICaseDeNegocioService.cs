using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICaseDeNegocioService : IService<CaseDeNegocio>
    {
        int Adicionar(CaseDTO caseDTO);

        void Atualizar(CaseDTO caseDTO);

        IEnumerable<CaseDTO> Listar(int? idCaseDeNegocio = null);
        
    }
}