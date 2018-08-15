using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}