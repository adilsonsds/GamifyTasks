using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IQuestaoRepository : IRepository<Questao>
    {
        IList<Questao> ListarPorCaseELicao(int idCase, int idLicao);

        
    }
}