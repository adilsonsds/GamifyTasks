using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class TrofeuRepository : Repository<Trofeu>, ITrofeuRepository
    {
        public TrofeuRepository(GamifyTasksContext context)
            : base(context)
        {

        }

        public IList<Trofeu> ListarPorCase(int idCaseDeNegocio)
        {
            return Queryable().Where(t => t.IdCase == idCaseDeNegocio).OrderBy(t => t.Nome).ToList();
        }
    }
}