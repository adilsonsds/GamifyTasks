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
        private readonly ILicaoRepository LicaoRepository;
        private readonly ITrofeuRepository TrofeuRepository;

        public Usuario UsuarioAutenticado { get; set; }

        public CaseController(ICaseDeNegocioRepository caseDeNegocioRepository, ILicaoRepository licaoRepository, ITrofeuRepository trofeuRepository, IUsuarioRepository usuarioRepository)
        {
            this.CaseDeNegocioRepository = caseDeNegocioRepository;
            this.LicaoRepository = licaoRepository;
            this.TrofeuRepository = trofeuRepository;

            UsuarioAutenticado = usuarioRepository.GetById(1);
        }

        #region CRUD Case de negócio

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var caseDeNegocio = CaseDeNegocioRepository.GetById(id);

            if (caseDeNegocio == null)
                return NotFound();

            var response = new CaseModel(caseDeNegocio);

            return Ok(response);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var casesDeNegocios = CaseDeNegocioRepository.ListarPorProfessor(UsuarioAutenticado.Id);
            var response = casesDeNegocios.Select(c => new CaseModel(c)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CaseModel caseModel)
        {
            if (caseModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = new CaseDeNegocio();
            caseModel.PreencherEntidade(caseDeNegocio);

            try
            {
                CaseDeNegocioRepository.Add(caseDeNegocio);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]CaseModel caseModel)
        {

            if (caseModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(caseModel.Id.Value);
            caseModel.PreencherEntidade(caseDeNegocio);

            try
            {
                CaseDeNegocioRepository.Update(caseDeNegocio);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        #endregion

        #region CRUD Lição

        [HttpGet("{idCase}/licao")]
        public ActionResult GetLicao(int idCase)
        {
            var licoes = LicaoRepository.ListarPorCase(idCase);

            var response = new
            {
                Licoes = licoes.Select(l => new LicaoModel(l)).ToList()
            };

            return Ok(response);
        }

        [HttpGet("{idCase}/licao/{idLicao}")]
        public ActionResult GetLicao(int idCase, int idLicao)
        {
            var licao = LicaoRepository.GetById(idLicao);

            if (licao == null)
                return NotFound();

            if (licao.IdCase != idCase)
                return BadRequest();

            var response = new LicaoModel(licao);

            return Ok(response);
        }

        [HttpPost("{idCase}/licao")]
        public ActionResult PostLicao(int idCase, [FromBody]LicaoModel licaoModel)
        {

            if (licaoModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(idCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Licao licao = new Licao();
            licao.IdCase = caseDeNegocio.Id;

            licaoModel.PreencherEntidade(licao);

            try
            {
                LicaoRepository.Add(licao);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("{idCase}/licao")]
        public ActionResult PutLicao(int idCase, [FromBody]LicaoModel licaoModel)
        {

            if (licaoModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(idCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Licao licao = LicaoRepository.GetById(licaoModel.Id.Value);

            if (licao == null || licao.IdCase != caseDeNegocio.Id)
                return BadRequest();

            licaoModel.PreencherEntidade(licao);

            try
            {
                LicaoRepository.Update(licao);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion

        #region CRUD Troféu

        [HttpGet("{idCase}/trofeu")]
        public ActionResult GetTrofeu(int idCase)
        {
            var trofeus = TrofeuRepository.ListarPorCase(idCase);

            var response = new
            {
                Trofeus = trofeus.Select(t => new TrofeuModel(t)).ToList()
            };

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
        public ActionResult PostTrofeu(int idCase, [FromBody]TrofeuModel trofeuModel)
        {

            if (trofeuModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(idCase);

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

            return Ok();
        }

        [HttpPost("{idCase}/trofeu")]
        public ActionResult PutTrofeu(int idCase, [FromBody]TrofeuModel trofeuModel)
        {

            if (trofeuModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(idCase);

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

            return Ok();
        }

        #endregion
    }
}
