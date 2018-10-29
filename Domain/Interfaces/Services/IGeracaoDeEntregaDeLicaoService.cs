using Domain.Entities;
using Domain.Request;

namespace Domain.Interfaces.Services
{
    public interface IGeracaoDeEntregaDeLicaoService : IService
    {
        int Gerar(int idCase, int idLicao, Usuario usuarioLogado);

        void Salvar(SalvarEntregaDeLicaoRequest request, Usuario usuarioLogado);
    }
}