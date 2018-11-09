using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class QuestaoParaAvaliarDTO
    {
        public QuestaoParaAvaliarDTO()
        {
            Responsaveis = new List<ResponsavelPelaLicaoDTO>();
        }

        public int IdQuestao { get; set; }
        public int IdResposta { get; set; }
        public string Resposta { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
        public bool EntregueForaDoPrazo { get; set; }
        public int? IdGrupo { get; set; }
        public IList<ResponsavelPelaLicaoDTO> Responsaveis { get; set; }
    }
}