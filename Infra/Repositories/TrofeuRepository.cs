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

        public IEnumerable<Trofeu> Listar(int idCaseDeNegocio)
        {
            return Queryable().Where(l => l.IdCase == idCaseDeNegocio).OrderBy(t => t.Nome).ToList();
        }
        
    }
}