using Api.Models.Usuario;
using Api.Security;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly UsuarioLogado _usuarioLogado;
        private readonly IEntregaDeTrofeuService _entregaDeTrofeuService;
        private readonly IConsultaDeAlunosService _consultaDeAlunosService;
        private readonly ICaseDeNegocioService _caseDeNegocioService;

        public UsuarioController(IUsuarioService usuarioService, UsuarioLogado usuarioLogado, IEntregaDeTrofeuService entregaDeTrofeuService,
            IConsultaDeAlunosService consultaDeAlunosService, ICaseDeNegocioService caseDeNegocioService)
        {
            _usuarioService = usuarioService;
            _usuarioLogado = usuarioLogado;
            _entregaDeTrofeuService = entregaDeTrofeuService;
            _consultaDeAlunosService = consultaDeAlunosService;
            _caseDeNegocioService = caseDeNegocioService;
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]UsuarioModel usuarioModel)
        {
            var usuario = new Usuario();
            usuarioModel.PreencherEntidade(usuario);

            try
            {
                _usuarioService.Adicionar(usuario);
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
            var usuario = _usuarioService.ObterPorId(usuarioModel.Id);

            if (usuario == null)
                return NotFound();

            usuarioModel.PreencherEntidade(usuario);

            try
            {
                _usuarioService.Atualizar(usuario);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{id}/trofeus")]
        public IActionResult Trofeus(int id)
        {
            try
            {
                var response = _entregaDeTrofeuService.Listar(new FiltroTrofeusRequest { IdUsuario = id });
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("cases")]
        public IActionResult Cases()
        {
            try
            {
                var lista = _caseDeNegocioService.ListarCasesDeNegocioAssociadosAoUsuario(_usuarioLogado.Obter().Id);
                return Ok(lista);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/cases")]
        public IActionResult Cases(int id)
        {
            try
            {
                var lista = _caseDeNegocioService.ListarCasesDeNegocioAssociadosAoUsuario(id);
                return Ok(lista);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/grupos")]
        public IActionResult Grupos(int id)
        {
            try
            {
                var response = _consultaDeAlunosService.ListarGruposOndeUsuarioEhMembro(idUsuario: id);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
