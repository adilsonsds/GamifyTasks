using Domain.Entities;

namespace Domain.DTO
{
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {

        }

        public UsuarioDTO(Usuario usuario)
        {
            Id = usuario.Id;
            NomeCompleto = usuario.NomeCompleto;
            Email = usuario.Email;
        }

        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
    }
}