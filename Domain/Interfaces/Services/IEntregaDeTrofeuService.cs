using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Request;

namespace Domain.Interfaces.Services
{
    public interface IEntregaDeTrofeuService : IService<EntregaDeTrofeu>
    {
        void Atribuir(AtribuirTrofeuRequest request, Usuario usuario);
        IList<EntregaDeTrofeuDTO> Listar(FiltroTrofeusRequest filtro);
        IList<KeyValuePair<int, EntregaDeTrofeuDTO>> ListarTrofeusRecebidosPelaLicaoEntregue(IQueryable<EntregaDeLicao> entregasDeLicoesFiltradas);
    }
}