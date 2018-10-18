using System;

namespace Domain.Entities
{
    public class AlunoDoCase : TEntity
    {
        public CaseDeNegocio Case { get; set; }
        public Usuario Aluno { get; set; }
    }
}