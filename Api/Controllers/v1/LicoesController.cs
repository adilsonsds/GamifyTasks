using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Security;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/cases/{idCase}/licoes")]
    public class LicoesController : ControllerBase
    {
        private readonly ILicaoService _licaoService;
        private readonly IGeracaoDeEntregaDeLicaoService _geracaoDeEntregaDeLicaoService;
        private readonly UsuarioLogado _usuarioLogado;

        public LicoesController(ILicaoService licaoService, UsuarioLogado usuarioLogado,
            IGeracaoDeEntregaDeLicaoService geracaoDeEntregaDeLicaoService)
        {
            _licaoService = licaoService;
            _usuarioLogado = usuarioLogado;
            _geracaoDeEntregaDeLicaoService = geracaoDeEntregaDeLicaoService;
        }

        [HttpGet]
        public ActionResult Get(int idCase)
        {
            var lista = _licaoService.Listar(idCase, _usuarioLogado.Obter());
            return Ok(lista);
        }

        [HttpGet("{idLicao}")]
        public ActionResult Get(int idCase, int idLicao)
        {
            var licao = _licaoService.Obter(idCase, idLicao, _usuarioLogado.Obter());
            return Ok(licao);
        }

        [HttpPost]
        public ActionResult Post([FromBody]LicaoDTO licaoDTO)
        {
            try
            {
                int idLicao = _licaoService.Adicionar(licaoDTO, _usuarioLogado.Obter());
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
                _licaoService.Atualizar(licaoDTO, _usuarioLogado.Obter());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{idLicao}/entregas")]
        public ActionResult GerarEntrega(int idCase, int idLicao)
        {
            try
            {
                int idEntrega = _geracaoDeEntregaDeLicaoService.Gerar(idCase, idLicao, _usuarioLogado.Obter());
                return Ok(idEntrega);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
