using System;
using System.Linq;
using Api.Models;
using Api.Models.Case;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/case")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseDeNegocioRepository CaseDeNegocioRepository;
        private readonly IUsuarioRepository UsuarioRepository;

        public CaseController(ICaseDeNegocioRepository caseDeNegocioRepository, IUsuarioRepository usuarioRepository)
        {
            this.CaseDeNegocioRepository = caseDeNegocioRepository;
            this.UsuarioRepository = usuarioRepository;
        }

        #region CRUD Case de negócio

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var caseDeNegocio = CaseDeNegocioRepository.GetById(id);

            if (caseDeNegocio == null)
                return NotFound();

            var response = new CaseResponse(caseDeNegocio);

            return Ok(response);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var cases = CaseDeNegocioRepository.Queryable();
            var usuarios = UsuarioRepository.Queryable();
            // cases = cases.Where(p => p.Nome.Contains("teste"));

            var testeComLinq = (from c in cases
                                join u in usuarios on c.IdProfessor equals u.Id
                                where c.Nome.Contains("teste")
                                select new
                                {
                                    Id = c.Id,
                                    Nome = c.Nome,
                                    Professor = u.NomeCompleto
                                }).ToList();

            // var resultado = cases
            //         .Select(c => new
            //         {
            //             Id = c.Id,
            //             Nome = c.Nome
            //         })
            //         .ToList();
            return Ok(testeComLinq);

            // var listaDeCases = CaseRepository.Get();
            // return Ok(listaDeCases);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CaseRequest request)
        {
            try
            {
                var professor = UsuarioRepository.GetById(1);

                CaseDeNegocio caseDeNegocio = new CaseDeNegocio();
                caseDeNegocio.IdProfessor = professor.Id;
                caseDeNegocio.Nome = request.Nome;
                caseDeNegocio.TextoDeApresentacao = request.TextoDeApresentacao;
                caseDeNegocio.PermiteMontarGrupos = request.PermiteMontarGrupos;
                caseDeNegocio.MinimoDeAlunosPorGrupo = request.MinimoDeAlunosPorGrupo;
                caseDeNegocio.MaximoDeAlunosPorGrupo = request.MaximoDeAlunosPorGrupo;

                CaseDeNegocioRepository.SaveOrUpdate(caseDeNegocio);
                return Ok(caseDeNegocio.Id);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao processar dados.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]CaseRequest request)
        {
            try
            {
                var professor = UsuarioRepository.GetById(1);
                var caseDeNegocio = CaseDeNegocioRepository.GetById(request.Id);

                if (caseDeNegocio.IdProfessor != professor.Id)
                    return Forbid();

                caseDeNegocio.Nome = request.Nome;
                caseDeNegocio.TextoDeApresentacao = request.TextoDeApresentacao;
                caseDeNegocio.PermiteMontarGrupos = request.PermiteMontarGrupos;
                caseDeNegocio.MinimoDeAlunosPorGrupo = request.MinimoDeAlunosPorGrupo;
                caseDeNegocio.MaximoDeAlunosPorGrupo = request.MaximoDeAlunosPorGrupo;

                CaseDeNegocioRepository.SaveOrUpdate(caseDeNegocio);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao processar dados.");
            }
        }

        #endregion

        #region CRUD Lição

        // [HttpGet("{id}/licao")]
        // public ActionResult GetLicao(int id)
        // {
        //     var caseDeNegocio = CaseDeNegocioRepository.GetById(id);

        //     if (caseDeNegocio == null)
        //         return NotFound();

        //     var response = new CaseResponse(caseDeNegocio);

        //     return Ok(response);
        // }


        #endregion


    }
}
