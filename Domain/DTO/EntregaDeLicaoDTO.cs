using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTO
{
    public class EntregaDeLicaoDTO
    {

        public EntregaDeLicaoDTO()
        {
            PermiteEditar = false;
            PermiteAvaliar = false;
            PermiteRealizar = false;
            Questoes = new List<QuestaoComRespostaDTO>();
            Responsaveis = new List<ResponsavelPelaLicaoDTO>();
        }

        public EntregaDeLicaoDTO(EntregaDeLicao entregaDeLicao) : this()
        {
            IdEntregaDeLicao = entregaDeLicao.Id;
            IdLicao = entregaDeLicao.Licao.Id;
            IdCase = entregaDeLicao.Licao.IdCase;
            NomeCase = entregaDeLicao.Licao.CaseDeNegocio.Nome;
            Titulo = entregaDeLicao.Licao.Titulo;
            TextoApresentacao = entregaDeLicao.Licao.TextoApresentacao;
            Descricao = entregaDeLicao.Licao.Descricao;
            FormaDeEntrega = entregaDeLicao.Licao.FormaDeEntrega;
            DataLiberacao = entregaDeLicao.Licao.DataLiberacao;
            DataEncerramento = entregaDeLicao.Licao.DataEncerramento;
            PermiteEntregasForaDoPrazo = entregaDeLicao.Licao.PermiteEntregasForaDoPrazo;

            foreach (var questao in entregaDeLicao.Licao.Questoes)
            {
                var resposta = entregaDeLicao.Respostas.FirstOrDefault(r => r.IdQuestao == questao.Id);
                Questoes.Add(new QuestaoComRespostaDTO(questao, resposta));
            }
        }

        public int IdEntregaDeLicao { get; set; }
        public int IdLicao { get; set; }
        public int IdCase { get; set; }
        public string NomeCase { get; set; }
        public string Titulo { get; set; }
        public string TextoApresentacao { get; set; }
        public string Descricao { get; set; }
        public FormaDeEntregaDaLicaoEnum FormaDeEntrega { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public bool PermiteEntregasForaDoPrazo { get; set; }
        public bool PermiteEditar { get; set; }
        public bool PermiteAvaliar { get; set; }
        public bool PermiteRealizar { get; set; }
        public bool PermiteEntregar { get; set; }
        public IList<QuestaoComRespostaDTO> Questoes { get; set; }
        public IList<ResponsavelPelaLicaoDTO> Responsaveis { get; set; }
    }
}