using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ILicaoRepository : IRepository<Licao>
    {
        IList<Licao> ListarPorCase(int idCase);

    }
}