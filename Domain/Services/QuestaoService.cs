using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.DTO;
using System.Collections.Generic;
using System;

namespace Domain.Services
{
    public class QuestaoService : BaseService<Questao>, IQuestaoService
    {
        private readonly IQuestaoRepository _questaoRepository;
        // private readonly ILicaoService _licaoService;

        public QuestaoService(IQuestaoRepository questaoRepository)//, ILicaoService licaoService)
            : base(questaoRepository)
        {
            _questaoRepository = questaoRepository;
            // _licaoService = licaoService;
        }

        public int Adicionar(QuestaoDTO questaoDTO)
        {
            // if (questaoDTO == null || questaoDTO.Id.HasValue)
            //     throw new Exception("Solicitação inválida.");

            // Licao licao = _licaoService.Obter(questaoDTO.IdCase, questaoDTO.IdLicao);

            // if (licao == null)
            //     throw new Exception("Lição não encontrada.");

            // Questao questao = new Questao();
            // questao.IdLicao = licao.Id;
            // questao.Licao = licao;

            // questaoDTO.PreencherEntidade(questao);

            // Adicionar(questao);

            // return licao.Id;
            throw new Exception("Não implementado");
        }

        public void Atualizar(QuestaoDTO questaoDTO)
        {
            // if (licaoDTO == null || !licaoDTO.Id.HasValue)
            //     throw new Exception("Solicitação inválida.");

            // Questao questao = Obter(licaoDTO.Id.Value);

            // if (licao == null || licao.IdCase != licaoDTO.IdCase)
            //     throw new Exception("Lição não encontrada.");

            // questaoDTO.PreencherEntidade(licao);

            // Atualizar(licao);
            throw new Exception("Não implementado");
        }

        public IEnumerable<QuestaoDTO> Listar(int idCaseDeNegocio, int idLicao, int? idQuestao = null)
        {
            var response = new List<QuestaoDTO>();

            var questoes = _questaoRepository.Listar(idCaseDeNegocio, idLicao, idQuestao);

            foreach (var questao in questoes)
            {
                var questaoDTO = new QuestaoDTO(questao);
                response.Add(questaoDTO);
            }

            return null;
        }

        
    }
}