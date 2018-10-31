using Domain.Entities;

namespace Domain.DTO
{
    public class MembroDoGrupoDTO
    {
        public MembroDoGrupoDTO()
        {
            
        }

        public MembroDoGrupoDTO(Usuario usuario, AlunoDoCase alunoDoCase)
        {
            IdAluno = alunoDoCase.Id;
            IdUsuario = usuario.Id;
            NomeCompleto = usuario.NomeCompleto;
        }

        public int IdUsuario { get; set; }
        public int IdAluno { get; set; }
        public string NomeCompleto { get; set; }
    }
}