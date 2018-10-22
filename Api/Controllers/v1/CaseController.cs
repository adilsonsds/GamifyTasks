using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using System;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Api.Security;

namespace Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/v1/cases")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly UsuarioLogado _usuarioLogado;

        public CaseController(ICaseDeNegocioService caseDeNegocioService, UsuarioLogado usuarioLogado)
        {
            _caseDeNegocioService = caseDeNegocioService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var lista = _caseDeNegocioService.Listar(_usuarioLogado.Obter());
            return Ok(lista);
        }

        [HttpGet("{idCase}")]
        public ActionResult Get(int idCase)
        {
            var lista = _caseDeNegocioService.ObterPorId(idCase, _usuarioLogado.Obter());
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]CaseDTO caseDTO)
        {
            try
            {
                int idCase = _caseDeNegocioService.Adicionar(caseDTO, _usuarioLogado.Obter());
                return Ok(idCase);
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
                _caseDeNegocioService.Atualizar(caseDTO, _usuarioLogado.Obter());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
