using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class AlunoDoCaseRepository : Repository<AlunoDoCase>, IAlunoDoCaseRepository
    {
        public AlunoDoCaseRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}