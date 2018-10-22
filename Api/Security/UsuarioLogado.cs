using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Api.Security
{
    public class UsuarioLogado
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioLogado(IHttpContextAccessor accessor, IUsuarioRepository usuarioRepository)
        {
            _accessor = accessor;
            _usuarioRepository = usuarioRepository;
        }

        public string Email => _accessor.HttpContext.User.Identity.Name;

        // public int Usuario => GetClaimsIdentity().FirstOrDefault(a => a.Type == "IdUsuario")?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public Domain.Entities.Usuario Obter()
        {
            if (!string.IsNullOrWhiteSpace(Email)) return _usuarioRepository.ObterPorEmail(Email);
            else return null;
        }
    }
}