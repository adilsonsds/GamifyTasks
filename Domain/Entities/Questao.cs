namespace Domain.Entities
{
    public class Questao : TEntity
    {
        public int IdLicao { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }
        public virtual Licao Licao { get; set; }
    }
}