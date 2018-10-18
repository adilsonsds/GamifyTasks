using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;

namespace Api.Controllers
{
    [Route("api/case/{idCase}/licao")]
    public class CaseLicoesController : ControllerBase
    {
        private readonly ILicaoService _licaoService;

        public CaseLicoesController(ILicaoService licaoService)
        {
            _licaoService = licaoService;
        }

        [HttpGet]
        public ActionResult Get(int idCase) => Ok(_licaoService.Listar(idCase));

        [HttpGet("{idLicao}")]
        public ActionResult Get(int idCase, int idLicao) => Ok(_licaoService.Listar(idCase, idLicao));

        [HttpPost]
        public ActionResult Post([FromBody]LicaoDTO licaoDTO)
        {
            try
            {
                int idLicao = _licaoService.Adicionar(licaoDTO);
                return Ok(idLicao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idLicao}")]
        public ActionResult Put([FromBody]LicaoDTO licaoDTO)
        {
            try
            {
                _licaoService.Atualizar(licaoDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
