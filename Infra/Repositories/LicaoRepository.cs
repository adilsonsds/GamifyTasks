using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class LicaoRepository : Repository<Licao>, ILicaoRepository
    {
        public LicaoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}