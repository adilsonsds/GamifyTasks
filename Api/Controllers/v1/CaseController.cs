using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using System;
using Domain.DTO;

namespace Api.Controllers
{
    [Route("api/v1/cases")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;

        public CaseController(ICaseDeNegocioService caseDeNegocioService)
        {
            _caseDeNegocioService = caseDeNegocioService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var lista = _caseDeNegocioService.Listar();
            return Ok(lista);
        }

        [HttpGet("{idCase}")]
        public ActionResult Get(int idCase)
        {
            var lista = _caseDeNegocioService.Listar(idCase);
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]CaseDTO caseDTO)
        {
            try
            {
                int idLicao = _caseDeNegocioService.Adicionar(caseDTO);
                return Ok(idLicao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idCase}")]
        public ActionResult Put([FromBody]CaseDTO caseDTO)
        {
            try
            {
                _caseDeNegocioService.Atualizar(caseDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
