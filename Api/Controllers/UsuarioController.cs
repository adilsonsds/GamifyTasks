using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository UsuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            this.UsuarioRepository = usuarioRepository;
        }

        // // GET api/values
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var usuario = UsuarioRepository.GetById(id);
            return Ok(usuario);
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult Get()
        {
            var usuarios = UsuarioRepository.Get();
            return Ok(usuarios);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]string nome, string sobrenome, string email, string senha)
        {
            var usuario = new Usuario
            {
                NomeCompleto = $"{nome} {sobrenome}",
                Email = email,
                Senha = senha
            };

            UsuarioRepository.Add(usuario);
            UsuarioRepository.Save();

            return Ok("Usuario criado com sucesso.");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id)//, [FromBody] string value)
        {
            var usuario = UsuarioRepository.GetById(id);
            usuario.Email = "adilson@dev2.com";

            UsuarioRepository.Update(usuario);
            UsuarioRepository.Save();

            return Ok("Usuario atualizado com sucesso.");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            UsuarioRepository.Remove(id);
            return Ok("Usuario removido com sucesso");
        }
    }
}
