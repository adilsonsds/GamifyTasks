using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {

        readonly GamifyTasksContext Context;

        public ContaController(GamifyTasksContext context)
        {
            this.Context = context;
        }
        // // GET api/values
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // GET api/values/5
        [HttpGet]
        public ActionResult Get()
        {
            var contas = Context.Contas.ToList();
            return Ok(contas);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post()//[FromBody] string value)
        {
            var conta = new Conta
            {
                Email = "adilson@dev.com",
                Senha = "123456"
            };

            Context.Add(conta);
            Context.SaveChanges();

            return Ok("Conta criada com sucesso.");
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
