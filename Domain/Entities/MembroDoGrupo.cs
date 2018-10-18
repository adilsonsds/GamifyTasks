using System;

namespace Domain.Entities
{
    public class MembroDoGrupo : TEntity
    {
        public Grupo Grupo { get; set; }
        public AlunoDoCase Aluno { get; set; }
    }
}