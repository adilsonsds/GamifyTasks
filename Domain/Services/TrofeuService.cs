using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class TrofeuService : BaseService<Trofeu>, ITrofeuService
    {
        private readonly ITrofeuRepository _trofeuRepository;
        private readonly ICaseDeNegocioService _caseDeNegocioService;

        public TrofeuService(ITrofeuRepository trofeuRepository, ICaseDeNegocioService caseDeNegocioService)
            : base(trofeuRepository)
        {
            _trofeuRepository = trofeuRepository;
            _caseDeNegocioService = caseDeNegocioService;
        }

        public int Adicionar(TrofeuDTO trofeuDTO)
        {
            if (trofeuDTO == null || trofeuDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            CaseDeNegocio caseDeNegocio = _caseDeNegocioService.ObterPorId(trofeuDTO.IdCase);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            Trofeu trofeu = new Trofeu();
            trofeu.IdCase = caseDeNegocio.Id;
            trofeu.CaseDeNegocio = caseDeNegocio;

            trofeuDTO.PreencherEntidade(trofeu);

            Adicionar(trofeu);

            return trofeu.Id;
        }

        public void Atualizar(TrofeuDTO trofeuDTO)
        {
            if (trofeuDTO == null || !trofeuDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            Trofeu trofeu = ObterPorId(trofeuDTO.Id.Value);

            if (trofeu == null || trofeu.IdCase != trofeuDTO.IdCase)
                throw new Exception("Troféu não encontrado.");

            trofeuDTO.PreencherEntidade(trofeu);

            Atualizar(trofeu);
        }

        public IEnumerable<TrofeuDTO> Listar(int idCaseDeNegocio, int? idTrofeu = null)
        {
            List<TrofeuDTO> response = new List<TrofeuDTO>();

            var trofeus = _trofeuRepository.Listar(idCaseDeNegocio, idTrofeu);

            foreach (var trofeu in trofeus)
            {
                TrofeuDTO trofeuDTO = new TrofeuDTO(trofeu);
                response.Add(trofeuDTO);
            }

            return response;
        }
    }
}