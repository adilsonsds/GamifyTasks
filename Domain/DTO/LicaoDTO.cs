using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTO
{
    public class LicaoDTO
    {
        public LicaoDTO()
        {
            PermiteEditar = false;
            PermiteAvaliar = false;
            PermiteRealizar = false;
            Questoes = new List<QuestaoDTO>();
            Responsaveis = new List<ResponsavelPelaLicaoDTO>();
        }

        public LicaoDTO(Licao licao) : this()
        {
            Id = licao.Id;
            IdCase = licao.IdCase;
            NomeCase = licao.CaseDeNegocio.Nome;
            Titulo = licao.Titulo;
            TextoApresentacao = licao.TextoApresentacao;
            Descricao = licao.Descricao;
            FormaDeEntrega = licao.FormaDeEntrega;
            DataLiberacao = licao.DataLiberacao;
            DataEncerramento = licao.DataEncerramento;
            PermiteEntregasForaDoPrazo = licao.PermiteEntregasForaDoPrazo;
            Questoes = licao.Questoes.Select(q => new QuestaoDTO(q)).ToList();
        }

        public LicaoDTO(EntregaDeLicao entregaDeLicao) : this()
        {
            IdEntregaDeLicao = entregaDeLicao.Id;
            Id = entregaDeLicao.Licao.Id;
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
                Resposta resposta = entregaDeLicao.Respostas.FirstOrDefault(r => r.IdQuestao == questao.Id);
                Questoes.Add(new QuestaoDTO(questao, resposta));
            }
        }

        public int? Id { get; set; }
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
        public int? IdEntregaDeLicao { get; set; }
        public bool PermiteRealizar { get; set; }
        public bool PermiteVisualizar { get; set; }
        public bool PermiteEntregar { get; set; }
        public bool EhProfessor { get; set; }
        public bool EhAluno { get; set; }
        public IList<QuestaoDTO> Questoes { get; set; }
        public bool Entregue { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
        public IList<ResponsavelPelaLicaoDTO> Responsaveis { get; set; }

        public void PreencherEntidade(Licao licao)
        {
            licao.Titulo = Titulo;
            licao.TextoApresentacao = TextoApresentacao;
            licao.Descricao = Descricao;
            licao.FormaDeEntrega = FormaDeEntrega;
            licao.DataLiberacao = DataLiberacao;
            licao.DataEncerramento = DataEncerramento;
        }
    }
}