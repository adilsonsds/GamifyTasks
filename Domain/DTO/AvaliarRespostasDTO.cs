using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.DTO
{
    public class AvaliarRespostasDTO
    {
        public AvaliarRespostasDTO()
        {
            Questoes = new List<AvaliarQuestaoDTO>();
        }

        public AvaliarRespostasDTO(Licao licao)
        {
            IdLicao = licao.Id;
            Titulo = licao.Titulo;
            TextoApresentacao = licao.TextoApresentacao;
            Questoes = licao.Questoes.Select(q => new AvaliarQuestaoDTO(q)).ToList();
        }

        public int IdLicao { get; set; }
        public string Titulo { get; set; }
        public string TextoApresentacao { get; set; }
        public IList<AvaliarQuestaoDTO> Questoes { get; set; }
    }

    public class AvaliarQuestaoDTO
    {
        public AvaliarQuestaoDTO(Questao questao)
        {
            IdQuestao = questao.Id;
            Titulo = questao.Titulo;
            NotaMaxima = questao.NotaMaxima;
        }

        public int IdQuestao { get; set; }
        public string Titulo { get; set; }
        public int NotaMaxima { get; set; }
    }
}