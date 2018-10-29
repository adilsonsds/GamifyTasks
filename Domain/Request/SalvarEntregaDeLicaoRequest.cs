using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Request
{
    public class SalvarEntregaDeLicaoRequest
    {
        public int IdEntregaDeLicao { get; set; }
        public EntregaDeLicaoStatusEnum Status { get; set; }
        public IList<QuestaoRequest> Questoes { get; set; }
    }

    public class QuestaoRequest
    {
        public int Id { get; set; }
        public string Resposta { get; set; }
    }
}