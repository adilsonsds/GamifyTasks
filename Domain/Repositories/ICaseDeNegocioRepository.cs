using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICaseDeNegocioRepository : IRepository<CaseDeNegocio>
    {
        void SaveOrUpdate(CaseDeNegocio caseDeNegocio);
    }
}