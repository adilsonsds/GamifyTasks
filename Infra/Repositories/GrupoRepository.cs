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

        public IEnumerable<Grupo> Listar(int? idGrupo = null)
        {
            var retorno = Queryable().ToList();
            return retorno;
        }
    }
}