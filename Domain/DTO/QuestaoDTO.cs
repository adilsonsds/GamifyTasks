using Domain.Entities;

namespace Domain.DTO
{
    public class QuestaoDTO
    {
        public QuestaoDTO()
        {

        }

        public QuestaoDTO(Questao questao)
        {
            Id = questao.Id;
            Titulo = questao.Titulo;
            NotaMaxima = questao.NotaMaxima;
        }

        public int? Id { get; set; }
        public int IdCase { get; set; }
        public int IdLicao { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }
        public bool PermiteEditar { get; set; }

        public void PreencherEntidade(Questao questao)
        {
            questao.Titulo = Titulo;
            questao.NotaMaxima = NotaMaxima;
        }
    }
}