using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}