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
        private readonly UsuarioLogado _usuarioLogado;

        public EntregasController(UsuarioLogado usuarioLogado, ILicaoService licaoService,
            IGeracaoDeEntregaDeLicaoService geracaoDeEntregasService)
        {
            _usuarioLogado = usuarioLogado;
            _licaoService = licaoService;
            _geracaoDeEntregasService = geracaoDeEntregasService;
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
    }
}