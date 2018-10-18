using System;
using System.Linq;
using System.Collections.Generic;
using Api.Models;
using Api.Models.Case;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;

namespace Api.Controllers
{
    [Route("api/case")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly ICaseDeNegocioRepository CaseDeNegocioRepository;
        private readonly ITrofeuRepository TrofeuRepository;

        public Usuario UsuarioAutenticado { get; set; }

        public CaseController(ICaseDeNegocioService caseDeNegocioService,
                              ICaseDeNegocioRepository caseDeNegocioRepository,
                              ITrofeuRepository trofeuRepository,
                              IUsuarioRepository usuarioRepository)
        {
            _caseDeNegocioService = caseDeNegocioService;

            this.CaseDeNegocioRepository = caseDeNegocioRepository;
            this.TrofeuRepository = trofeuRepository;

            UsuarioAutenticado = usuarioRepository.Queryable().OrderByDescending(u => u.Id).FirstOrDefault();
        }

        #region CRUD Case de negócio

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var caseDeNegocio = _caseDeNegocioService.ObterPorId(id);

            if (caseDeNegocio == null)
                return NotFound();

            return Ok(new CaseModel(caseDeNegocio));
        }

        [HttpGet]
        public ActionResult Get()
        {
            var casesDeNegocios = CaseDeNegocioRepository.ListarPorProfessor(UsuarioAutenticado.Id);
            var response = casesDeNegocios.Select(c => new CaseModel(c)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public ActionResult Post([FromBody]CaseModel caseModel)
        {
            if (caseModel == null || caseModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = new CaseDeNegocio();
            caseDeNegocio.IdProfessor = UsuarioAutenticado.Id;
            caseModel.PreencherEntidade(caseDeNegocio);

            try
            {
                CaseDeNegocioRepository.Add(caseDeNegocio);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(caseDeNegocio.Id);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody]CaseModel caseModel)
        {
            if (caseModel == null || caseModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(caseModel.Id.Value);

            if (caseDeNegocio == null)
                return NotFound();

            caseModel.PreencherEntidade(caseDeNegocio);

            try
            {
                CaseDeNegocioRepository.Update(caseDeNegocio);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        #endregion

        #region CRUD Troféu

        [HttpGet("{idCase}/trofeu")]
        public ActionResult GetTrofeu(int idCase)
        {
            var trofeus = TrofeuRepository.ListarPorCase(idCase);
            var response = trofeus.Select(t => new TrofeuModel(t)).ToList();

            return Ok(response);
        }

        [HttpGet("{idCase}/trofeu/{idTrofeu}")]
        public ActionResult GetTrofeu(int idCase, int idTrofeu)
        {
            var trofeu = TrofeuRepository.GetById(idTrofeu);

            if (trofeu == null)
                return NotFound();

            if (trofeu.IdCase != idCase)
                return BadRequest();

            var response = new TrofeuModel(trofeu);

            return Ok(response);
        }

        [HttpPost("{idCase}/trofeu")]
        public ActionResult PostTrofeu([FromBody]TrofeuModel trofeuModel)
        {
            if (trofeuModel == null || trofeuModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(trofeuModel.IdCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Trofeu trofeu = new Trofeu();
            trofeu.IdCase = caseDeNegocio.Id;

            trofeuModel.PreencherEntidade(trofeu);

            try
            {
                TrofeuRepository.Add(trofeu);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(trofeu.Id);
        }

        [HttpPut("{idCase}/trofeu/{idTrofeu}")]
        public ActionResult PutTrofeu([FromBody]TrofeuModel trofeuModel)
        {
            if (trofeuModel == null || trofeuModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(trofeuModel.IdCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Trofeu trofeu = TrofeuRepository.GetById(trofeuModel.Id.Value);

            if (trofeu == null || trofeu.IdCase != caseDeNegocio.Id)
                return BadRequest();

            trofeuModel.PreencherEntidade(trofeu);

            try
            {
                TrofeuRepository.Update(trofeu);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        #endregion
    }
}
