using Api.Models.Usuario;
using Api.Security;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository UsuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly UsuarioLogado _usuarioLogado;

        public UsuarioController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService,
            UsuarioLogado usuarioLogado)
        {
            this.UsuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet("{idUsuario}")]
        public ActionResult Get(int idUsuario)
        {
            try
            {
                var response = _usuarioService.Obter(idUsuario);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var usuario = _usuarioLogado.Obter();
                var response = _usuarioService.Obter(usuario.Id);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
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
