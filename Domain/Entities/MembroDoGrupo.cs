using System;

namespace Domain.Entities
{
    public class MembroDoGrupo
    {
        public int Id { get; set; }
        public Grupo Grupo { get; set; }
        public AlunoDoCase Aluno { get; set; }
    }
}