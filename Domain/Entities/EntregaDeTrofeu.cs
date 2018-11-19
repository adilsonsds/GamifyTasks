namespace Domain.Entities
{
    public class EntregaDeTrofeu : TEntity
    {
        public EntregaDeTrofeu()
        {

        }

        public EntregaDeTrofeu(Trofeu trofeu, EntregaDeLicao entregaDeLicao, Resposta resposta)
        {
            IdTrofeu = trofeu.Id;
            IdEntregaDeLicao = entregaDeLicao.Id;

            if (resposta != null)
                IdResposta = resposta.Id;
        }

        public int IdTrofeu { get; set; }
        public int IdEntregaDeLicao { get; set; }
        public int? IdResposta { get; set; }
    }
}