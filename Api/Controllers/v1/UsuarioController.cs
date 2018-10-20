using System;
using Api.Models.Usuario;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/usuario")]
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

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var usuarios = UsuarioRepository.GetAll();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioModel usuarioModel)
        {
            var usuario = new Usuario();
            usuarioModel.PreencherEntidade(usuario);

            try
            {
                UsuarioRepository.Add(usuario);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(usuario.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]UsuarioModel usuarioModel)
        {
            var usuario = UsuarioRepository.GetById(usuarioModel.Id);

            if (usuario == null)
                return NotFound();

            usuarioModel.PreencherEntidade(usuario);

            try
            {
                UsuarioRepository.Update(usuario);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
