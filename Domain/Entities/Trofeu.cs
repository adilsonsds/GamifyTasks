namespace Domain.Entities
{
    public class Trofeu
    {
        public int Id { get; set; }
        public Case Case { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pontos { get; set; }
    }
}