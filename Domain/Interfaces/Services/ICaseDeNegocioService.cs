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

        bool UsuarioEstaAssociadoAoCaseDeNegocioComoProfessor(Usuario usuario, CaseDeNegocio caseDeNegocio);

        bool UsuarioEstaInscritoNoCaseDeNegocio(Usuario usuario, CaseDeNegocio caseDeNegocio);

        bool PermiteUsuarioEditarCaseDeNegocio(Usuario usuario, CaseDeNegocio caseDeNegocio);

        int? Localizar(LocalizarCaseRequest request);
    }
}