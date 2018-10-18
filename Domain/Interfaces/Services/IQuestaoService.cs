using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IQuestaoService : IService<Questao>
    {
        int Adicionar(QuestaoDTO questaoDTO);

        void Atualizar(QuestaoDTO questaoDTO);

        IEnumerable<QuestaoDTO> Listar(int idCaseDeNegocio, int idLicao, int? idQuestao = null);
    }
}