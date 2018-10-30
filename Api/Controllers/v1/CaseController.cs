using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using System;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Api.Security;

namespace Api.Controllers.v1
{
    [Authorize("Bearer")]
    [Route("api/v1/cases")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly UsuarioLogado _usuarioLogado;
        private readonly IConsultaDeAlunosService _consultaDeAlunosService;
        private readonly IGrupoService _grupoService;

        public CaseController(ICaseDeNegocioService caseDeNegocioService, UsuarioLogado usuarioLogado,
            IConsultaDeAlunosService consultaDeAlunosService, IGrupoService grupoService)
        {
            _caseDeNegocioService = caseDeNegocioService;
            _usuarioLogado = usuarioLogado;
            _consultaDeAlunosService = consultaDeAlunosService;
            _grupoService = grupoService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var lista = _caseDeNegocioService.ListarCasesDeNegocioAssociadosAoUsuario(_usuarioLogado.Obter());
                return Ok(lista);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{idCase}")]
        public ActionResult Get(int idCase)
        {
            try
            {
                var response = _caseDeNegocioService.ObterDetalhesPorId(idCase, _usuarioLogado.Obter());
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]CaseDetalhesDTO caseDTO)
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
        public ActionResult Put([FromBody]CaseDetalhesDTO caseDTO)
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

        [HttpGet("localizar")]
        public ActionResult Localizar(int id)
        {
            try
            {
                var response = _caseDeNegocioService.Localizar(new LocalizarCaseRequest { Id = id });
                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("{idCase}/inscrever")]
        public ActionResult Inscrever(int idCase)
        {
            try
            {
                _caseDeNegocioService.InscreverUsuarioNoCaseDeNegocio(idCase, _usuarioLogado.Obter());
                return NoContent();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("{idCase}/alunos")]
        public ActionResult Alunos(int idCase)
        {
            try
            {
                var response = _consultaDeAlunosService.ListarAlunosPorCase(idCase);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{idCase}/grupos")]
        public ActionResult Grupos(int idCase)
        {
            try
            {
                var response = _grupoService.ListarPorCaseDeNegocio(idCase);
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
