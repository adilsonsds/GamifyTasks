using Domain.Entities;
using Domain.Interfaces.Repositories;

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