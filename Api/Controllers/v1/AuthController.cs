using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDTO loginDTO)
        {
            try
            {
                _usuarioService.Autenticar(loginDTO);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            return NoContent();
        }

    }
}
