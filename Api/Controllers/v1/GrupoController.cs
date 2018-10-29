using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/cases/{idCase}/grupo")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;

        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        public ActionResult Get(int idCase)
        {
            var lista = _grupoService.Listar(idCase);
            return Ok(lista);
        }

         [HttpGet("{idGrupo}")]
        public ActionResult Get(int idCase, int idGrupo)
        {
            var grupo = _grupoService.ObterPorId(idCase, idGrupo);
            return Ok(grupo);
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
