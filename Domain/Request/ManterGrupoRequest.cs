using System.Collections.Generic;

namespace Domain.Request
{
    public class ManterGrupoRequest
    {
        ManterGrupoRequest()
        {
            IdsAlunosMembros = new List<int>();
        }

        public int? IdGrupo { get; set; }
        public int IdCase { get; set; }
        public string Nome { get; set; }
        public string GritoDeGuerra { get; set; }
        public IList<int> IdsAlunosMembros { get; set; }
    }
}