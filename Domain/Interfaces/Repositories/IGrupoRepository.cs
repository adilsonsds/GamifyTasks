using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        IEnumerable<Grupo> Listar(int? idGrupo = null);
    }
}