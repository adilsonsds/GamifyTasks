using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntregaDeTrofeuRepository : IRepository<EntregaDeTrofeu>
    {
        IList<EntregaDeTrofeu> ListarPorEntregaDeLicao(int idEntregaDeLicao);
    }
}