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

        public IEnumerable<Licao> Listar(int idCaseDeNegocio, int? idLicao = null)
        {
            var query = Queryable().Where(l => l.IdCase == idCaseDeNegocio);

            if (idLicao.HasValue && idLicao > 0)
                query = query.Where(l => l.Id == idLicao);

            return query.OrderBy(l => l.Id).ToList();
        }
    }
}