namespace Domain.Entities
{
    public class EntregaDeTrofeu : TEntity
    {
        public Trofeu Trofeu { get; set; }
        public EntregaDeLicao EntregaDeLicao { get; set; }
        public Resposta Resposta { get; set; }
    }
}