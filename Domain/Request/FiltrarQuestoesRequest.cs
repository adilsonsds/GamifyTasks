namespace Domain.Request
{
    public class FiltrarQuestoesRequest
    {
        public FiltrarQuestoesRequest()
        {
            
        }
        
        public FiltrarQuestoesRequest(int idLicao, int idQuestao, bool removerQuestoesJaAvaliadas)
        {
            IdLicao = idLicao;
            IdQuestao = idQuestao;
            RemoverQuestoesJaAvaliadas = removerQuestoesJaAvaliadas;
        }

        public int IdLicao { get; set; }
        public int IdQuestao { get; set; }
        public bool RemoverQuestoesJaAvaliadas { get; set; }
    }
}