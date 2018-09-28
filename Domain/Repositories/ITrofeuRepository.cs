using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITrofeuRepository : IRepository<Trofeu>
    {
        IList<Trofeu> ListarPorCase(int idCaseDeNegocio);
    }
}