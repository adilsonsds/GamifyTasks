using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        void Autenticar(LoginDTO loginDTO);
        
    }
}