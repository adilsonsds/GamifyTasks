using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class EntregaDeLicaoService : BaseService<EntregaDeLicao>, IEntregaDeLicaoService
    {
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;

        public EntregaDeLicaoService(IEntregaDeLicaoRepository entregaDeLicaoRepository)
            : base(entregaDeLicaoRepository)
        {
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
        }

        public void Manter(EntregaDeLicao entregaDeLicao)
        {
            if (entregaDeLicao.Id > 0)
                Atualizar(entregaDeLicao);
            else
                Adicionar(entregaDeLicao);

        }
    }
}