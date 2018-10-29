using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ILicaoRepository : IRepository<Licao>
    {
        IEnumerable<Licao> ListarPorCaseDeNegocio(int idCaseDeNegocio);

    }
}