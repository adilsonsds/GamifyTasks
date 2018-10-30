using System;

namespace Domain.Entities
{
    public class MembroDoGrupo : TEntity
    {
        public MembroDoGrupo()
        {

        }

        public MembroDoGrupo(Grupo grupo, AlunoDoCase aluno)
        {
            IdGrupo = grupo.Id;
            IdAluno = aluno.Id;
        }

        public int IdGrupo { get; set; }
        public int IdAluno { get; set; }
    }
}