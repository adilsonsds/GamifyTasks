using System;

namespace Domain.Entities
{
    public class Resposta
    {
        public int Id { get; set; }
        public EntregaDeLicao EntregaDeLicao { get; set; }
        public Questao Questao { get; set; }
        public string Conteudo { get; set; }
        public int PontosGanhos { get; set; }
    }
}