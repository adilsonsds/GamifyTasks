using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}