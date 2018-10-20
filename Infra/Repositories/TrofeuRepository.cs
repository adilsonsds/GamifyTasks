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

        public IEnumerable<Trofeu> Listar(int idCaseDeNegocio, int? idTrofeu = null)
        {
            var query = Queryable().Where(l => l.IdCase == idCaseDeNegocio);

            if (idTrofeu.HasValue && idTrofeu > 0)
                query = query.Where(t => t.Id == idTrofeu);

            return query.OrderBy(t => t.Nome).ToList();
        }
        
    }
}