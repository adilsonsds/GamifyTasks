using System;

namespace Domain.Entities
{
    public class AlunoDoCase : TEntity
    {
        public AlunoDoCase()
        {

        }

        public AlunoDoCase(CaseDeNegocio caseDeNegocio, Usuario usuario)
        {
            IdCaseDeNegocio = caseDeNegocio.Id;
            IdUsuario = usuario.Id;
        }


        public int IdCaseDeNegocio { get; set; }
        public int IdUsuario { get; set; }
    }
}