using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class CaseDeNegocioRepository : Repository<CaseDeNegocio>, ICaseDeNegocioRepository
    {
        public CaseDeNegocioRepository(GamifyTasksContext context)
            : base(context)
        {
        }

        public void SaveOrUpdate(CaseDeNegocio caseDeNegocio)
        {
            if (caseDeNegocio.Id > 0)
                Update(caseDeNegocio);
            else
                Add(caseDeNegocio);
        }
    }
}