using System;
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
            PermiteEntregar = false;
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