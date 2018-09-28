using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Api.Models.Case
{
    public class LicaoModel
    {
        public LicaoModel()
        {

        }

        public LicaoModel(Licao licao)
        {
            Id = licao.Id;
            Titulo = licao.Titulo;
            TextoApresentacao = licao.TextoApresentacao;
            Descricao = licao.Descricao;
            FormaDeEntrega = licao.FormaDeEntrega;
            DataLiberacao = licao.DataLiberacao;
            DataEncerramento = licao.DataEncerramento;
            PermiteEntregasForaDoPrazo = licao.PermiteEntregasForaDoPrazo;
        }

        public int? Id { get; set; }
        public string Titulo { get; set; }
        public string TextoApresentacao { get; set; }
        public string Descricao { get; set; }
        public FormaDeEntregaDaLicaoEnum FormaDeEntrega { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public bool PermiteEntregasForaDoPrazo { get; set; }

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