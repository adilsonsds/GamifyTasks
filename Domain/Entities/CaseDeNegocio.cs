using System.Collections.Generic;

namespace Domain.Entities
{
    public class CaseDeNegocio : TEntity
    {
        public CaseDeNegocio()
        {
        }

        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoDeAlunosPorGrupo { get; set; }
        public int? MaximoDeAlunosPorGrupo { get; set; }
        public int IdProfessor { get; set; }
        public virtual Usuario Professor { get; set; }
    }
}