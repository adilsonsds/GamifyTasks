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

            return new UsuarioDTO(usuario);;
        }

        public UsuarioDTO Obter(int idUsuario)
        {
            var usuario = _usuarioRepository.GetById(idUsuario);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            return new UsuarioDTO(usuario);
        }
    }
}