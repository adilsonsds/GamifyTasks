using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using System;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Api.Security;
using Domain.Request;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/avaliacoes")]
    public class AvaliacoesController : ControllerBase
    {
        private readonly IAvaliacaoDeRespostasService _avaliacaoService;
        private readonly UsuarioLogado _usuarioLogado;

        public AvaliacoesController(IAvaliacaoDeRespostasService avaliacaoService, UsuarioLogado usuarioLogado)
        {
            _avaliacaoService = avaliacaoService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet("{idLicao}")]
        public IActionResult Preparar(int idLicao)
        {
            try
            {
                var response = _avaliacaoService.ObterDadosDePreparacaoParaAvaliarRespostas(idLicao, _usuarioLogado.Obter());
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{idLicao}/questoes")]
        public IActionResult FiltrarQuestoes(int idLicao, int idQuestao, bool removerJaAvaliadas)
        {
            try
            {
                var response = _avaliacaoService.ListarQuestoesParaAvaliar(new FiltrarQuestoesRequest(idLicao, idQuestao, removerJaAvaliadas));
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("nota")]
        public IActionResult AtribuirNota([FromBody]AtribuirNotaRequest request)
        {
            try
            {
                _avaliacaoService.AtribuirPontosParaResposta(request, _usuarioLogado.Obter());
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
