using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Request;

namespace Domain.Interfaces.Services
{
    public interface IAvaliacaoDeRespostasService : IService
    {
        AvaliarRespostasDTO ObterDadosDePreparacaoParaAvaliarRespostas(int idLicao, Usuario usuarioLogado);
        IList<QuestaoParaAvaliarDTO> ListarQuestoesParaAvaliar(FiltrarQuestoesRequest filtro);
        void AtribuirPontosParaResposta(AtribuirNotaRequest request, Usuario usuarioLogado);
    }
}