using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class CaseDeNegocio : TEntity
    {
        public CaseDeNegocio()
        {
            // Licoes = new List<Licao>();
        }

        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoDeAlunosPorGrupo { get; set; }
        public int? MaximoDeAlunosPorGrupo { get; set; }
        public string ChaveDeBusca { get; set; }
        public int IdProfessor { get; set; }
        public virtual Usuario Professor { get; set; }

        // public virtual ICollection<Licao> Licoes { get; set; }

        public void GerarChaveDeBusca()
        {
            Random rdm = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 6;
            String chaveRandomica = new string(Enumerable.Repeat(chars, length).Select(s => s[rdm.Next(s.Length)]).ToArray());
            ChaveDeBusca = chaveRandomica;
        }
    }
}