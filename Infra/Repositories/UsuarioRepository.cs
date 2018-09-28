using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(GamifyTasksContext context)
            : base(context) { }

        public Usuario ObterPorEmailESenha(string email, string senha)
        {
            return Queryable().FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}