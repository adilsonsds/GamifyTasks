using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class RespostaRepository : Repository<Resposta>, IRespostaRepository
    {
        public RespostaRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}