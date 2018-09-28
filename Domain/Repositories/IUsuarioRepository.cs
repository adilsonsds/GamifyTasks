using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterPorEmailESenha(string email, string senha);
    }
}