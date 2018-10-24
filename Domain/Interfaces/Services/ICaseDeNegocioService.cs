using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICaseDeNegocioService : IService<CaseDeNegocio>
    {
        int Adicionar(CaseDetalhesDTO caseDTO, Usuario usuario);

        void Atualizar(CaseDetalhesDTO caseDTO, Usuario usuario);

        IEnumerable<CaseDTO> ListarCasesDeNegocioAssociadosAoUsuario(Usuario usuario);
        
        CaseDetalhesDTO ObterDetalhesPorId(int idCaseDeNegocio, Usuario usuario);
        
        void InscreverUsuarioNoCaseDeNegocio(int idCaseDeNegocio, Usuario usuario);
    }
}