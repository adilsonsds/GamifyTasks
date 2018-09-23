using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class QuestaoRepository : Repository<Questao>, IQuestaoRepository
    {
        public QuestaoRepository(GamifyTasksContext context)
            : base(context)
        {
            
        }
    }
}