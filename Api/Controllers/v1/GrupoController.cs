using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Domain.Request;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/grupos")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;

        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet("{idGrupo}")]
        public ActionResult Get(int idGrupo)
        {
            var lista = _grupoService.ObterDetalhes(idGrupo);
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]ManterGrupoRequest request)
        {
            try
            {
                int idGrupo = _grupoService.Adicionar(request);
                return Ok(idGrupo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idGrupo}")]
        public ActionResult Put([FromBody]ManterGrupoRequest request)
        {
            try
            {
                _grupoService.Atualizar(request);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
