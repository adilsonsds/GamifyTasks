using System.Collections.Generic;

namespace Domain.Entities
{
    public class EntregaDeLicao
    {
        public int Id { get; set; }
        public Licao Licao { get; set; }
        public IList<ResponsavelPelaLicao> Responsaveis { get; set; }
        public IList<Resposta> Respostas { get; set; }
        public IList<EntregaDeTrofeu> Trofeus { get; set; }
    }
}