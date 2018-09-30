using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class QuestaoRepository : Repository<Questao>, IQuestaoRepository
    {
        public QuestaoRepository(GamifyTasksContext context)
            : base(context) { }

        public void SaveOrUpdate(Questao questao)
        {
            if (questao.Id > 0)
                Update(questao);
            else
                Add(questao);
        }
        
        public IList<Questao> ListarPorCaseELicao(int idCase, int idLicao)
        {
            // var licao = LicaoRepository.Queryable();
            var questao = Queryable();

            var lista = (from q in questao
                             //  join l in licao on q.IdLicao equals l.Id
                         where q.IdLicao == idLicao //&& l.IdCase == idCase
                         select q).ToList();

            return lista;
        }
    }
}