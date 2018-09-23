using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class MembroDoGrupoRepository : Repository<MembroDoGrupo>, IMembroDoGrupoRepository
    {
        public MembroDoGrupoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}