using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICaseDeNegocioService : IService<CaseDeNegocio>
    {
        int Adicionar(CaseDTO caseDTO, Usuario usuarioLogado);

        void Atualizar(CaseDTO caseDTO, Usuario usuarioLogado);

        IEnumerable<CaseDTO> Listar(Usuario usuarioLogado, int? idCaseDeNegocio = null);
        
        CaseDTO ObterPorId(int idCaseDeNegocio, Usuario usuarioLogado);
        
    }
}