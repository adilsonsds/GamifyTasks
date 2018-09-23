using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class EntregaDeTrofeuRepository : Repository<EntregaDeTrofeu>, IEntregaDeTrofeuRepository
    {
        public EntregaDeTrofeuRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}