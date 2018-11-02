using System;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class AvaliacaoDeRespostasService : BaseService, IAvaliacaoDeRespostasService
    {
        private readonly ICaseDeNegocioService _caseDeNegocioService;
        private readonly ILicaoRepository _licaoRepository;
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;

        public AvaliacaoDeRespostasService(ICaseDeNegocioService caseDeNegocioService, ILicaoRepository licaoRepository,
            IEntregaDeLicaoRepository entregaDeLicaoRepository)
        {
            _caseDeNegocioService = caseDeNegocioService;
            _licaoRepository = licaoRepository;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
        }

        public AvaliarRespostasDTO ObterDadosDePreparacaoParaAvaliarRespostas(int idCase, int idLicao, Usuario usuarioLogado)
        {
            Licao licao = _licaoRepository.GetById(idLicao);

            if (licao == null || licao.IdCase != idCase)
                throw new Exception("Lição não encontrada.");

            bool ehProfessor = _caseDeNegocioService.UsuarioEstaAssociadoAoCaseDeNegocioComoProfessor(usuarioLogado, licao.CaseDeNegocio);

            if (!ehProfessor)
                throw new Exception("Apenas professores têm permissão para avaliar as lições entregues.");

            return new AvaliarRespostasDTO(licao);
        }
    }
}