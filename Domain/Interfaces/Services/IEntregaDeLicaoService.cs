using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IEntregaDeLicaoService : IService<EntregaDeLicao>
    {
        void Manter(EntregaDeLicao entregaDeLicao);
    }
}