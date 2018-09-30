using System;
using System.Linq;
using System.Collections.Generic;
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

            UsuarioAutenticado = usuarioRepository.Queryable().OrderByDescending(u => u.Id).FirstOrDefault();
        }

        #region CRUD Case de negócio

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var caseDeNegocio = CaseDeNegocioRepository.GetById(id);
            var professor = UsuarioAutenticado; //TODO alterar

            if (caseDeNegocio == null)
                return NotFound();

            return Ok(new CaseModel(caseDeNegocio, professor));
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

            var questoes = QuestaoRepository.ListarPorCaseELicao(idCase, idLicao);
            var caseDeNegocio = CaseDeNegocioRepository.GetById(licao.IdCase);
            var response = new LicaoModel(licao, caseDeNegocio, questoes);

            return Ok(response);
        }

        [HttpPost("{id}/licao")]
        public ActionResult PostLicao([FromBody]LicaoModel licaoModel)
        {

            if (licaoModel == null || licaoModel.Id > 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(licaoModel.IdCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Licao licao = new Licao();
            licao.IdCase = caseDeNegocio.Id;

            licaoModel.PreencherEntidade(licao);

            try
            {
                LicaoRepository.Add(licao);

                ManterQuestoes(licao, licaoModel.Questoes, new List<Questao>());
            }
            catch
            {
                return BadRequest();
            }

            return Ok(licao.Id);
        }

        [HttpPut("{idCase}/licao/{idLicao}")]
        public ActionResult PutLicao([FromBody]LicaoModel licaoModel)
        {

            if (licaoModel == null || licaoModel.Id <= 0)
                return BadRequest();

            CaseDeNegocio caseDeNegocio = CaseDeNegocioRepository.GetById(licaoModel.IdCase);

            if (caseDeNegocio == null)
                return BadRequest();

            Licao licao = LicaoRepository.GetById(licaoModel.Id.Value);

            if (licao == null || licao.IdCase != caseDeNegocio.Id)
                return BadRequest();

            licaoModel.PreencherEntidade(licao);

            try
            {
                LicaoRepository.Update(licao);

                var questoesExistentes = QuestaoRepository.ListarPorCaseELicao(caseDeNegocio.Id, licao.Id);
                ManterQuestoes(licao, licaoModel.Questoes, questoesExistentes);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        private void ManterQuestoes(Licao licao, IList<QuestaoModel> questoesParaSalvar, IList<Questao> questoesExistentes)
        {
            foreach (var questaoModel in questoesParaSalvar)
            {
                Questao questao = questoesExistentes.FirstOrDefault(q => q.Id == questaoModel.Id);

                if (questao == null)
                    questao = new Questao
                    {
                        IdLicao = licao.Id
                    };

                questaoModel.PreencherEntidade(questao);

                QuestaoRepository.SaveOrUpdate(questao);
            }

            var questoesRemovidas = questoesExistentes.Where(qe => !questoesParaSalvar.Any(qs => qs.Id == qe.Id)).ToList();
            foreach (var questaoParaRemover in questoesRemovidas)
            {
                QuestaoRepository.Remove(questaoParaRemover.Id);
            }
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
