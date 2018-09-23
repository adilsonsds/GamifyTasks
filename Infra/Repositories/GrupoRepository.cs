using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}