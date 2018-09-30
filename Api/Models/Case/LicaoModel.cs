using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Api.Models.Case
{
    public class LicaoModel
    {
        public LicaoModel()
        {
            Questoes = new List<QuestaoModel>();
        }

        public LicaoModel(Licao licao) : this()
        {
            Id = licao.Id;
            IdCase = licao.IdCase;
            Titulo = licao.Titulo;
            TextoApresentacao = licao.TextoApresentacao;
            Descricao = licao.Descricao;
            FormaDeEntrega = licao.FormaDeEntrega;
            DataLiberacao = licao.DataLiberacao;
            DataEncerramento = licao.DataEncerramento;
            PermiteEntregasForaDoPrazo = licao.PermiteEntregasForaDoPrazo;
            PermiteEditar = true;
            PermiteAvaliar = true;
            PermiteRealizar = true;
            PermiteEntregar = true;
        }

        public LicaoModel(Licao licao, CaseDeNegocio caseDeNegocio, IList<Questao> questoes) : this(licao)
        {
            NomeCase = caseDeNegocio.Nome;
            Questoes = questoes.Select(q => new QuestaoModel(q)).ToList();
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
        public bool PermiteRealizar { get; set; }
        public bool PermiteEntregar { get; set; }
        public List<QuestaoModel> Questoes { get; set; }

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