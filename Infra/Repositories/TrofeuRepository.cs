using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class TrofeuRepository : Repository<Trofeu>, ITrofeuRepository
    {
        public TrofeuRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}