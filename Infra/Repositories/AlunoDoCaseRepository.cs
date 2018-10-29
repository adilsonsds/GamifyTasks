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
            return FiltrarPorUsuarioECaseDeNegocio(idUsuario, idCaseDeNegocio).Any();
        }

        public AlunoDoCase Obter(int idUsuario, int idCaseDeNegocio)
        {
            return FiltrarPorUsuarioECaseDeNegocio(idUsuario, idCaseDeNegocio).FirstOrDefault();
        }


        #region Métodos privados
        private IQueryable<AlunoDoCase> FiltrarPorUsuarioECaseDeNegocio(int idUsuario, int idCaseDeNegocio)
        {
            return Queryable().Where(a => a.IdUsuario == idUsuario && a.IdCaseDeNegocio == idCaseDeNegocio);
        }
        #endregion
    }
}