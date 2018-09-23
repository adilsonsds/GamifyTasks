using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class ResponsavelPelaLicaoRepository : Repository<ResponsavelPelaLicao>, IResponsavelPelaLicaoRepository
    {
        public ResponsavelPelaLicaoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}