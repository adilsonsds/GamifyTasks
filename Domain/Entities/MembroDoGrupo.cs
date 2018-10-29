using System;

namespace Domain.Entities
{
    public class MembroDoGrupo : TEntity
    {
        public int IdGrupo { get; set; }
        public int IdAluno { get; set; }
    }
}