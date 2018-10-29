using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class LicaoRepository : Repository<Licao>, ILicaoRepository
    {
        public LicaoRepository(GamifyTasksContext context)
            : base(context)
        {

        }

        public IEnumerable<Licao> ListarPorCaseDeNegocio(int idCaseDeNegocio)
        {
            return Queryable()
                .Where(l => l.IdCase == idCaseDeNegocio)
                .OrderBy(l => l.Id)
                .ToList();
        }
    }
}