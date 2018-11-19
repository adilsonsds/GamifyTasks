using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Security;
using Domain.Request;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/entregas")]
    public class EntregasController : ControllerBase
    {
        private readonly ILicaoService _licaoService;
        private readonly IGeracaoDeEntregaDeLicaoService _geracaoDeEntregasService;
        private readonly IEntregaDeTrofeuService _entregaDeTrofeuService;
        private readonly UsuarioLogado _usuarioLogado;

        public EntregasController(UsuarioLogado usuarioLogado, ILicaoService licaoService,
            IGeracaoDeEntregaDeLicaoService geracaoDeEntregasService, IEntregaDeTrofeuService entregaDeTrofeuService)
        {
            _usuarioLogado = usuarioLogado;
            _licaoService = licaoService;
            _geracaoDeEntregasService = geracaoDeEntregasService;
            _entregaDeTrofeuService = entregaDeTrofeuService;
        }

        [HttpGet("{idEntrega}")]
        public ActionResult Get(int idEntrega)
        {
            try
            {
                var response = _licaoService.ObterEntrega(idEntrega, _usuarioLogado.Obter());
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{idEntrega}")]
        public ActionResult Post([FromBody]SalvarEntregaDeLicaoRequest request)
        {
            try
            {
                _geracaoDeEntregasService.Salvar(request, _usuarioLogado.Obter());
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}/trofeus")]
        public IActionResult ListarTrofeus(int id)
        {
            try
            {
                var response = _entregaDeTrofeuService.Listar(new FiltroTrofeusRequest { IdEntregaDeLicao = id });
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("trofeus")]
        public IActionResult AtribuirTrofeu([FromBody]AtribuirTrofeuRequest request)
        {
            try
            {
                _entregaDeTrofeuService.Atribuir(request, _usuarioLogado.Obter());
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}