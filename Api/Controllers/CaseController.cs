using System;
using System.Linq;
using Api.Models;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/case")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseRepository CaseRepository;
        private readonly IUsuarioRepository UsuarioRepository;

        public CaseController(ICaseRepository caseRepository, IUsuarioRepository usuarioRepository)
        {
            this.CaseRepository = caseRepository;
            this.UsuarioRepository = usuarioRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var caseDeNegocio = CaseRepository.GetById(id);
            return Ok(caseDeNegocio);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var cases = CaseRepository.Queryable();
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
        public IActionResult Post([FromBody]CaseModel caseModel)
        {
            Case caseDeNegocio;


            if (caseModel.Id > 0)
                caseDeNegocio = CaseRepository.GetById(caseModel.Id);
            else
            {
                var professor = UsuarioRepository.GetById(1);

                caseDeNegocio = new Case
                {
                    // Professor = professor,
                    IdProfessor = professor.Id
                };
            }

            caseDeNegocio.Nome = caseModel.Nome;
            caseDeNegocio.TextoDeApresentacao = caseModel.TextoDeApresentacao;
            caseDeNegocio.PermiteMontarGrupos = caseModel.PermiteMontarGrupos;
            caseDeNegocio.MinimoDeAlunosPorGrupo = caseModel.MinimoDeAlunosPorGrupo;
            caseDeNegocio.MaximoDeAlunosPorGrupo = caseModel.MaximoDeAlunosPorGrupo;

            try
            {
                if (caseDeNegocio.Id > 0)
                {
                    CaseRepository.Add(caseDeNegocio);
                    // CaseRepository.Save();
                }
                else
                {
                    CaseRepository.Update(caseDeNegocio);
                    // CaseRepository.Save();
                }

                return Ok(caseDeNegocio.Id);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao manter case de negócio.");
            }

        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]UsuarioModel usuarioModel)
        {
            try
            {
                //var case = CaseRepository.GetById(usuarioModel.Id);


                // UsuarioRepository.Update(usuario);
                // UsuarioRepository.Save();

                return Ok("Usuario atualizado com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível salvar os dados.");
            }
        }
    }
}
