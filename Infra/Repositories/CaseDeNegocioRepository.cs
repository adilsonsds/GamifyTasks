using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class CaseDeNegocioRepository : Repository<CaseDeNegocio>, ICaseDeNegocioRepository
    {
        public CaseDeNegocioRepository(GamifyTasksContext context)
            : base(context)
        {
        }

        public void SaveOrUpdate(CaseDeNegocio caseDeNegocio)
        {
            if (caseDeNegocio.Id > 0)
                Update(caseDeNegocio);
            else
                Add(caseDeNegocio);
        }

        public IList<CaseDeNegocio> ListarPorProfessor(int idProfessor)
        {
            return Queryable().Where(c => c.IdProfessor == idProfessor).OrderBy(c => c.Nome).ToList();
        }
    }
}