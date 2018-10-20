namespace Domain.Entities
{
    public class Trofeu : TEntity
    {
        public Trofeu()
        {
            
        }
        
        public int IdCase { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pontos { get; set; }
        public virtual CaseDeNegocio CaseDeNegocio { get; set; }
    }
}