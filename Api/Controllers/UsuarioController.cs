using System;
using Api.Models;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository UsuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            this.UsuarioRepository = usuarioRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var usuario = UsuarioRepository.GetById(id);
            return Ok(usuario);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var usuarios = UsuarioRepository.Get();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioModel usuarioModel)
        {
            var usuario = new Usuario
            {
                NomeCompleto = $"{usuarioModel.Nome} {usuarioModel.Sobrenome}",
                Email = usuarioModel.Email,
                Senha = usuarioModel.Senha
            };

            try
            {
                UsuarioRepository.Add(usuario);
                UsuarioRepository.Save();
                return Ok(usuario.Id);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao criar usuário.");
            }

        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]UsuarioModel usuarioModel)
        {
            try
            {
                var usuario = UsuarioRepository.GetById(usuarioModel.Id);

                usuario.NomeCompleto = $"{usuarioModel.Nome} {usuarioModel.Sobrenome}";
                usuario.Email = usuarioModel.Email;
                usuario.Senha = usuarioModel.Senha;

                UsuarioRepository.Update(usuario);
                UsuarioRepository.Save();

                return Ok("Usuario atualizado com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível salvar os dados.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            UsuarioRepository.Remove(id);
            return Ok("Usuario removido com sucesso");
        }
    }
}
