using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class AlunoDoCaseRepository : Repository<AlunoDoCase>, IAlunoDoCaseRepository
    {
        public AlunoDoCaseRepository(GamifyTasksContext context)
            : base(context)
        {

        }

        public bool UsuarioEstaAssociadoAoCaseDeNegocio(int idUsuario, int idCaseDeNegocio)
        {
            return Queryable().Any(a => a.IdUsuario == idUsuario && a.IdCaseDeNegocio == idCaseDeNegocio);
        }
    }
}