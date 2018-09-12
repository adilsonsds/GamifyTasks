using System;

namespace Domain.Entities
{
    public class AlunoDoCase
    {
        public int Id { get; set; }
        public Case Case { get; set; }
        public Usuario Aluno { get; set; }
    }
}