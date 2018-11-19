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
        private readonly IEntregaDeTrofeuService _entregaDeTrofeuService;
        private readonly UsuarioLogado _usuarioLogado;

        public GrupoController(IGrupoService grupoService, UsuarioLogado usuarioLogado, IEntregaDeTrofeuService entregaDeTrofeuService)
        {
            _grupoService = grupoService;
            _usuarioLogado = usuarioLogado;
            _entregaDeTrofeuService = entregaDeTrofeuService;
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
            try
            {
                var response = _grupoService.ObterDadosParaMontagemDeGrupos(_usuarioLogado.Obter(), idcase, grupo);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/trofeus")]
        public IActionResult Trofeus(int id)
        {
            // try
            // {
                var response = _entregaDeTrofeuService.Listar(new FiltroTrofeusRequest { IdGrupo = id });
                return Ok(response);
            // }
            // catch
            // {
            //     return NotFound();
            // }
        }
    }
}
