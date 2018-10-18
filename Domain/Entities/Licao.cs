using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class Licao : TEntity
    {
        public Licao()
        {
            FormaDeEntrega = FormaDeEntregaDaLicaoEnum.SomenteIndividual;
            PermiteEntregasForaDoPrazo = true;
        }

        public int IdCase { get; set; }
        public string Titulo { get; set; }
        public string TextoApresentacao { get; set; }
        public string Descricao { get; set; }
        public FormaDeEntregaDaLicaoEnum FormaDeEntrega { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public bool PermiteEntregasForaDoPrazo { get; set; }
        public virtual CaseDeNegocio CaseDeNegocio { get; set; }
    }
}