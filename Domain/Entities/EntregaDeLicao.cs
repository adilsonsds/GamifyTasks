using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class EntregaDeLicao : TEntity
    {
        public EntregaDeLicao()
        {
            Status = EntregaDeLicaoStatusEnum.NaoEntregue;
            Responsaveis = new List<ResponsavelPelaLicao>();
            Respostas = new List<Resposta>();
        }

        public EntregaDeLicao(Licao licao) : this()
        {
            IdLicao = licao.Id;
            Licao = licao;
        }

        public EntregaDeLicaoStatusEnum Status { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
        public int IdLicao { get; set; }
        public int? IdGrupo { get; set; }
        public virtual Licao Licao { get; set; }
        public virtual ICollection<ResponsavelPelaLicao> Responsaveis { get; set; }
        public virtual ICollection<Resposta> Respostas { get; set; }

        // public virtual ICollection<EntregaDeTrofeu> Trofeus { get; set; }
    }
}