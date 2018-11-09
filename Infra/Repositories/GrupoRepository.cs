using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System.Linq;

namespace Infra.Repositories
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(GamifyTasksContext context)
            : base(context)
        {

        }

        public IList<KeyValuePair<int, string>> ListarKeyValueDeGrupos(int idCaseDeNegocio)
        {
            return (from g in Queryable()
                    where g.IdCase == idCaseDeNegocio
                    select new KeyValuePair<int, string>(g.Id, g.Nome))
                    .ToList();
        }
    }
}