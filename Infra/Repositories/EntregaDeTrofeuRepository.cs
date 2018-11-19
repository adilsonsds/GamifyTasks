using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class EntregaDeTrofeuRepository : Repository<EntregaDeTrofeu>, IEntregaDeTrofeuRepository
    {
        public EntregaDeTrofeuRepository(GamifyTasksContext context)
            : base(context)
        {

        }

        public IList<EntregaDeTrofeu> ListarPorEntregaDeLicao(int idEntregaDeLicao)
        {
            return Queryable().Where(e => e.IdEntregaDeLicao == idEntregaDeLicao).ToList();
        }
    }
}