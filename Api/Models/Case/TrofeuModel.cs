using Domain.Entities;

namespace Api.Models.Case
{
    public class TrofeuModel
    {
        public TrofeuModel()
        {

        }

        public TrofeuModel(Trofeu trofeu)
        {
            Id = trofeu.Id;
            IdCase = trofeu.IdCase;
            Nome = trofeu.Nome;
            Descricao = trofeu.Descricao;
            Pontos = trofeu.Pontos;
        }

        public int? Id { get; set; }
        public int IdCase { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pontos { get; set; }

        public void PreencherEntidade(Trofeu trofeu)
        {
            trofeu.Nome = Nome;
            trofeu.Descricao = Descricao;
            trofeu.Pontos = Pontos;
        }
    }
}