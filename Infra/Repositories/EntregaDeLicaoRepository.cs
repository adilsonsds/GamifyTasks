using Domain.Entities;
using Domain.Repositories;

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