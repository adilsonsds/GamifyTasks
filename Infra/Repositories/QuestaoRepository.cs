using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class QuestaoRepository : Repository<Questao>, IQuestaoRepository
    {
        public QuestaoRepository(GamifyTasksContext context)
            : base(context) { }


        public IEnumerable<Questao> Listar(int idLicao, int? idQuestao = null)
        {
            var questoes = Queryable().Where(q => q.Licao.Id == idLicao);

            if (idQuestao.HasValue && idQuestao > 0)
                questoes = questoes.Where(q => q.Id == idQuestao);

            return questoes.OrderBy(q => q.Id).ToList();
        }
    }
}