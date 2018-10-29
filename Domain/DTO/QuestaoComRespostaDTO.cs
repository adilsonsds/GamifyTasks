using Domain.Entities;

namespace Domain.DTO
{
    public class QuestaoComRespostaDTO
    {
        public QuestaoComRespostaDTO(Questao questao, Resposta resposta)
        {
            Id = questao.Id;
            Titulo = questao.Titulo;
            NotaMaxima = questao.NotaMaxima;

            if (resposta != null)
            {
                Resposta = resposta.Conteudo;
                PontosGanhos = resposta.PontosGanhos;
            }
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }
        public string Resposta { get; set; }
        public int? PontosGanhos { get; set; }
    }
}