using System;
using Api.Models.Auth;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository UsuarioRepository;

        public AuthController(IUsuarioRepository usuarioRepository)
        {
            this.UsuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginModel loginModel)
        {
            Usuario usuario = UsuarioRepository.ObterPorEmailESenha(loginModel.Email, loginModel.Senha);

            if (usuario == null)
                return NotFound();

            return NoContent();
        }


        [HttpPost("/logout")]
        public ActionResult Logout()
        {
            return NoContent();
        }

    }
}
