using Domain.Entities;

namespace Api.Models.Case
{
    public class QuestaoModel
    {
        public QuestaoModel()
        {

        }

        public QuestaoModel(Questao questao)
        {
            Id = questao.Id;
            Titulo = questao.Titulo;
            NotaMaxima = questao.NotaMaxima;
        }

        public int? Id { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }

        public void PreencherEntidade(Questao questao)
        {
            questao.Titulo = Titulo;
            questao.NotaMaxima = NotaMaxima;
        }
    }
}