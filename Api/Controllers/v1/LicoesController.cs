using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;

namespace Api.Controllers
{
    [Route("api/v1/cases/{idCase}/licoes")]
    public class LicoesController : ControllerBase
    {
        private readonly ILicaoService _licaoService;

        public LicoesController(ILicaoService licaoService)
        {
            _licaoService = licaoService;
        }

        [HttpGet]
        public ActionResult Get(int idCase)
        {
            var lista = _licaoService.Listar(idCase);
            return Ok(lista);
        }

        [HttpGet("{idLicao}")]
        public ActionResult Get(int idCase, int idLicao)
        {
            var licao = _licaoService.Obter(idCase, idLicao);
            return Ok(licao);
        }

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
