using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Domain.Request;
using Api.Security;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/grupos")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;
        private readonly UsuarioLogado _usuarioLogado;

        public GrupoController(IGrupoService grupoService, UsuarioLogado usuarioLogado)
        {
            _grupoService = grupoService;
            _usuarioLogado = usuarioLogado;
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

        [HttpGet("novosmembros")]
        public ActionResult NovosMembros(int idcase, string aluno)
        {
            try
            {
                var response = _grupoService.PesquisarNovosMembros(idcase, aluno);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("preparar")]
        public ActionResult PrepararMontagens(int? idcase = null, int? grupo = null)
        {
            // try
            // {
                var response = _grupoService.ObterDadosParaMontagemDeGrupos(_usuarioLogado.Obter(), idcase, grupo);
                return Ok(response);
            // }
            // catch
            // {
            //     return BadRequest();
            // }
        }

    }
}
