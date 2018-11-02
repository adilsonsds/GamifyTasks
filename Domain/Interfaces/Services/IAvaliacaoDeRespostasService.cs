using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAvaliacaoDeRespostasService : IService
    {
        AvaliarRespostasDTO ObterDadosDePreparacaoParaAvaliarRespostas(int idCase, int idLicao, Usuario usuarioLogado);
        
    }
}