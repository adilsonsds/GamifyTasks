using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/v1/grupo")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;

        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var lista = _grupoService.Listar();
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]GrupoDTO grupoDTO)
        {
            try
            {
                int idLicao = _grupoService.Adicionar(grupoDTO);
                return Ok(idLicao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idGrupo}")]
        public ActionResult Put([FromBody]GrupoDTO grupoDTO)
        {
            try
            {
                _grupoService.Atualizar(grupoDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
