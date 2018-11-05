using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Security;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/cases/{idCase}/trofeus")]
    public class TrofeusController : ControllerBase
    {
        private readonly ITrofeuService _trofeuService;
        private readonly UsuarioLogado _usuarioLogado;

        public TrofeusController(ITrofeuService trofeuService, UsuarioLogado usuarioLogado)
        {
            _trofeuService = trofeuService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet]
        public ActionResult Get(int idCase)
        {
            var lista = _trofeuService.Listar(idCase);
            return Ok(lista);
        }

        [HttpGet("{idTrofeu}")]
        public ActionResult Get(int idCase, int idTrofeu)
        {
            var lista = _trofeuService.Obter(idCase, idTrofeu);
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody]TrofeuDTO trofeuDTO)
        {
            try
            {
                int id = _trofeuService.Adicionar(trofeuDTO, _usuarioLogado.Obter());
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idTrofeu}")]
        public ActionResult Put([FromBody]TrofeuDTO trofeuDTO)
        {
            try
            {
                _trofeuService.Atualizar(trofeuDTO, _usuarioLogado.Obter());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
