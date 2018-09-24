namespace Domain.Entities
{
    public class Trofeu
    {
        public int Id { get; set; }
        public int IdCase { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pontos { get; set; }
    }
}