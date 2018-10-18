using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class EntregaDeLicaoRepository : Repository<EntregaDeLicao>, IEntregaDeLicaoRepository
    {
        public EntregaDeLicaoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}