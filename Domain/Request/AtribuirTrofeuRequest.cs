namespace Domain.Request
{
    public class AtribuirTrofeuRequest
    {
        public int IdTrofeu { get; set; }
        public int IdEntregaDeLicao { get; set; }
        public int? IdResposta { get; set; }
    }
}