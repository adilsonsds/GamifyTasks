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
        private readonly IQuestaoRepository QuestaoRepository;
        private readonly ITrofeuRepository TrofeuRepository;

        public Usuario UsuarioAutenticado { get; set; }

        public CaseController(ICaseDeNegocioRepository caseDeNegocioRepository,
                              ILicaoRepository licaoRepository,
                              IQuestaoRepository questaoRepository,
                              ITrofeuRepository trofeuRepository,
                              IUsuarioRepository usuarioRepository)
        {
            this.CaseDeNegocioRepository = caseDeNegocioRepository;
            this.LicaoRepository = licaoRepository;
            this.QuestaoRepository = questaoRepository;
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

            return Ok(caseDeNegocio.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put([FromBody]CaseModel caseModel)
        {
            if (caseModel.Id <= 0)
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

        #region CRUD Lição

        [HttpGet("{idCase}/licao")]
        public ActionResult GetLicao(int idCase)
        {
            var licoes = LicaoRepository.ListarPorCase(idCase);

            return Ok(new
            {
                Licoes = licoes.Select(l => new LicaoModel(l)).ToList()
            });
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

            return Ok(licao.Id);
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

            return NoContent();
        }
        #endregion

        #region CRUD Questão

        [HttpGet("{idCase}/licao/{idLicao}/questao")]
        public ActionResult GetQuestoes(int idCase, int idLicao)
        {
            var questoes = QuestaoRepository.ListarPorCaseELicao(idCase, idLicao);

            var response = new
            {
                Questoes = questoes.Select(q => new QuestaoModel(q)).ToList()
            };

            return Ok(response);
        }

        [HttpGet("{idCase}/licao/{idLicao}/questao/{idQuestao}")]
        public ActionResult GetQuestao(int idCase, int idLicao, int idQuestao)
        {
            var questao = QuestaoRepository.GetById(idQuestao);

            if (questao == null)
                return NotFound();

            var licao = LicaoRepository.GetById(idLicao);

            if (licao == null || questao.IdLicao != licao.Id || licao.IdCase != idCase)
                return BadRequest();

            var response = new QuestaoModel(questao);

            return Ok(response);
        }

        [HttpPost("{idCase}/licao/{idLicao}/questao")]
        public ActionResult PostQuestao(int idCase, int idLicao, [FromBody]QuestaoModel questaoModel)
        {
            if (questaoModel.Id > 0)
                return BadRequest();

            Licao licao = LicaoRepository.GetById(idLicao);

            if (licao == null || licao.Id != idLicao || licao.IdCase != idCase)
                return BadRequest();

            Questao questao = new Questao();
            questao.IdLicao = licao.Id;

            questaoModel.PreencherEntidade(questao);

            try
            {
                QuestaoRepository.Add(questao);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(questao.Id);
        }

        [HttpPost("{idCase}/licao/{idLicao}/questao/{idQuestao}")]
        public ActionResult PutQuestao(int idCase, int idLicao, [FromBody]QuestaoModel questaoModel)
        {

            if (questaoModel.Id <= 0)
                return BadRequest();

            Questao questao = QuestaoRepository.GetById(questaoModel.Id.Value);

            if (questao == null || questao.IdLicao != idLicao)
                return BadRequest();

            Licao licao = LicaoRepository.GetById(idLicao);

            if (licao == null || licao.IdCase != idCase)
                return BadRequest();

            questaoModel.PreencherEntidade(questao);

            try
            {
                QuestaoRepository.Update(questao);
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

            return Ok(trofeu.Id);
        }

        [HttpPost("{idCase}/trofeu/{idTrofeu}")]
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

            return NoContent();
        }

        #endregion
    }
}
