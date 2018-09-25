using System.Collections.Generic;

namespace Domain.Entities
{
    public class CaseDeNegocio
    {
        public CaseDeNegocio()
        {
            // Licoes = new List<Licao>();
            // Alunos = new List<AlunoDoCase>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoDeAlunosPorGrupo { get; set; }
        public int? MaximoDeAlunosPorGrupo { get; set; }
        public int IdProfessor { get; set; }
        
        
        // public Usuario Professor { get; set; }
        // public IList<Licao> Licoes { get; set; }
        // public IList<AlunoDoCase> Alunos { get; set; }
    }
}