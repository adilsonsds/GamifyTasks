using Domain.Entities;

namespace Domain.DTO
{
    public class EntregaDeTrofeuDTO
    {
        public EntregaDeTrofeuDTO()
        {
        }

        public int IdEntrega { get; set; }
        public int IdTrofeu { get; set; }
        public string NomeTrofeu { get; set; }
        public int PontosMovimentados { get; set; }
    }
}