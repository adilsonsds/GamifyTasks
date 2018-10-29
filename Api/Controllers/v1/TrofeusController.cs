using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/cases/{idCase}/trofeus")]
    public class TrofeusController : ControllerBase
    {
        private readonly ITrofeuService _trofeuService;

        public TrofeusController(ITrofeuService trofeuService)
        {
            _trofeuService = trofeuService;
        }

        [HttpGet]
        public ActionResult Get(int idCase)
        {
            var lista = _trofeuService.Listar(idCase);
            return Ok(lista);
        }

        [HttpGet("{idQuestao}")]
        public ActionResult Get(int idCase, int idQuestao)
        {
            var lista = _trofeuService.Listar(idCase, idQuestao);
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]TrofeuDTO trofeuDTO)
        {
            try
            {
                int id = _trofeuService.Adicionar(trofeuDTO);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idQuestao}")]
        public ActionResult Put([FromBody]TrofeuDTO trofeuDTO)
        {
            try
            {
                _trofeuService.Atualizar(trofeuDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
