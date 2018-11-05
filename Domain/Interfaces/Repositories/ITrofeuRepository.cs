using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ITrofeuRepository : IRepository<Trofeu>
    {
        IEnumerable<Trofeu> Listar(int idCaseDeNegocio);
    }
}