namespace Domain.Entities
{
    public class EntregaDeTrofeu
    {
        public int Id { get; set; }
        public Trofeu Trofeu { get; set; }
        public EntregaDeLicao EntregaDeLicao { get; set; }
        public Resposta Resposta { get; set; }
    }
}