using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAlunoDoCaseRepository : IRepository<AlunoDoCase>
    {
        bool UsuarioEstaAssociadoAoCaseDeNegocio(int idUsuario, int idCaseDeNegocio);
    }
}