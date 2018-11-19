using System;

namespace Domain.Entities
{
    public class Resposta : TEntity
    {
        public int IdEntregaDeLicao { get; set; }
        public int IdQuestao { get; set; }
        // public int NumeroDaQuestao { get; set; }
        public string Conteudo { get; set; }
        public int? PontosGanhos { get; set; }
    }
}