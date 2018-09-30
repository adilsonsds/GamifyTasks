using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class LicaoRepository : Repository<Licao>, ILicaoRepository
    {
        public LicaoRepository(GamifyTasksContext context)
            : base(context)
        {
        }

        public IList<Licao> ListarPorCase(int idCase)
        {
            return Queryable().Where(l => l.IdCase == idCase).OrderBy(l => l.Id).ToList();
        }
    }
}