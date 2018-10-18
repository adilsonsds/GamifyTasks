using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterPorEmailESenha(string email, string senha);
    }
}