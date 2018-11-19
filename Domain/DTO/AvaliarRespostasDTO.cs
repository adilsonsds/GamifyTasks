using System;
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
            Trofeus = new List<TrofeuParaAtribuirDTO>();
        }

        public AvaliarRespostasDTO(Licao licao, IEnumerable<Trofeu> trofeus)
        {
            IdLicao = licao.Id;
            Titulo = licao.Titulo;
            TextoApresentacao = licao.TextoApresentacao;
            DataLiberacao = licao.DataLiberacao;
            DataEncerramento = licao.DataEncerramento;
            Questoes = licao.Questoes.Select(q => new AvaliarQuestaoDTO(q)).ToList();
            Trofeus = trofeus.Select(t => new TrofeuParaAtribuirDTO(t)).ToList();
        }

        public int IdLicao { get; set; }
        public string Titulo { get; set; }
        public string TextoApresentacao { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public IList<AvaliarQuestaoDTO> Questoes { get; set; }
        public List<TrofeuParaAtribuirDTO> Trofeus { get; set; }
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

    public class TrofeuParaAtribuirDTO
    {
        public TrofeuParaAtribuirDTO(Trofeu trofeu)
        {
            IdTrofeu = trofeu.Id;
            Nome = trofeu.Nome;
            Pontos = trofeu.Pontos;
        }

        public int IdTrofeu { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
    }
}