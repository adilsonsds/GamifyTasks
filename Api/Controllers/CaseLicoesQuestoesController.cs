using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTO;

namespace Api.Controllers
{
    [Route("api/case/{idCase}/licao/{idLicao}")]
    public class CaseLicoesQuestoesController : ControllerBase
    {
        private readonly IQuestaoService _questaoService;

        public CaseLicoesQuestoesController(IQuestaoService questaoService)
        {
            _questaoService = questaoService;
        }

        [HttpGet]
        public ActionResult Get(int idCase, int idLicao) => Ok(_questaoService.Listar(idCase, idLicao));

        [HttpGet("{idQuestao}")]
        public ActionResult Get(int idCase, int idLicao, int idQuestao) => Ok(_questaoService.Listar(idCase, idLicao, idQuestao));

        [HttpPost]
        public ActionResult Post([FromBody]QuestaoDTO questaoDTO)
        {
            try
            {
                int id = _questaoService.Adicionar(questaoDTO);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]QuestaoDTO questaoDTO)
        {
            try
            {
                _questaoService.Atualizar(questaoDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // private void ManterQuestoes(Licao licao, IList<QuestaoModel> questoesParaSalvar, IList<Questao> questoesExistentes)
        // {
        //     foreach (var questaoModel in questoesParaSalvar)
        //     {
        //         Questao questao = questoesExistentes.FirstOrDefault(q => q.Id == questaoModel.Id);

        //         if (questao == null)
        //             questao = new Questao
        //             {
        //                 IdLicao = licao.Id
        //             };

        //         questaoModel.PreencherEntidade(questao);

        //         QuestaoRepository.SaveOrUpdate(questao);
        //     }

        //     var questoesRemovidas = questoesExistentes.Where(qe => !questoesParaSalvar.Any(qs => qs.Id == qe.Id)).ToList();
        //     foreach (var questaoParaRemover in questoesRemovidas)
        //     {
        //         QuestaoRepository.Remove(questaoParaRemover.Id);
        //     }
        // }

    }
}
