using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        IList<KeyValuePair<int, string>> ListarKeyValueDeGrupos(int idCaseDeNegocio);
    }
}