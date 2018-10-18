using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestaoRepository : IRepository<Questao>
    {
        IEnumerable<Questao> Listar(int idCaseDeNegocio, int idLicao, int? idQuestao = null);        
    }
}