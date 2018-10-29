using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        UsuarioDTO Autenticar(LoginDTO loginDTO);
        
        UsuarioDTO Obter(int idUsuario);
        
    }
}