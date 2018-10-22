using System;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioDTO Autenticar(LoginDTO loginDTO)
        {
            Usuario usuario = _usuarioRepository.ObterPorEmailESenha(loginDTO.Email, loginDTO.Senha);

            if (usuario == null)
                throw new Exception("Login e/ou senha inválidos.");

            var usuarioDTO = new UsuarioDTO
            {
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email
            };

            return usuarioDTO;
        }
        
    }
}