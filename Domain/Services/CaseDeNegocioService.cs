using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class CaseDeNegocioService : BaseService<CaseDeNegocio>, ICaseDeNegocioService
    {
        public CaseDeNegocioService(ICaseDeNegocioRepository repository)
            : base(repository)
        {
        }

        public void Teste()
        {

        }
    }
}