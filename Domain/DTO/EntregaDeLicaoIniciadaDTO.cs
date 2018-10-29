using System;
using Domain.Enums;

namespace Domain.DTO
{
    public class EntregaDeLicaoIniciadaDTO
    {
        public int IdLicao { get; set; }
        public int IdEntregaDeLicao { get; set; }
        public EntregaDeLicaoStatusEnum Status { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
    }
}