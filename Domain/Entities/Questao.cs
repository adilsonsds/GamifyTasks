namespace Domain.Entities
{
    public class Questao
    {
        public int Id { get; set; }
        public Licao Licao { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }
    }
}