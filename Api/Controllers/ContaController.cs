using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository ContaRepository;

        public ContaController(IContaRepository contaRepository)
        {
            this.ContaRepository = contaRepository;
        }

        // // GET api/values
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var conta = ContaRepository.GetById(id);
            return Ok(conta);
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult Get()
        {
            var contas = ContaRepository.Get();
            return Ok(contas);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post()//[FromBody] string value)
        {
            var conta = new Conta
            {
                Email = "adilson@dev1.com",
                Senha = "123456"
            };

            ContaRepository.Add(conta);
            ContaRepository.Save();

            return Ok("Conta criada com sucesso.");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id)//, [FromBody] string value)
        {
            var conta = ContaRepository.GetById(id);
            conta.Email = "adilson@dev2.com";

            ContaRepository.Update(conta);
            ContaRepository.Save();

            return Ok("Conta atualizada com sucesso.");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ContaRepository.Remove(id);
            return Ok("Conta removida com sucesso");
        }
    }
}
