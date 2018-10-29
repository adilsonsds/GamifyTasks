using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ConsultaEntregaDeLicaoService : BaseService, IConsultaEntregaDeLicaoService
    {
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;
        private readonly IResponsavelPelaLicaoRepository _responsavelRepository;
        private readonly IAlunoDoCaseRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ConsultaEntregaDeLicaoService(IEntregaDeLicaoRepository entregaDeLicaoRepository,
            IResponsavelPelaLicaoRepository responsavelRepository, IAlunoDoCaseRepository alunoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
            _responsavelRepository = responsavelRepository;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool AlunoJaComecouAFazerALicao(int idAluno, int idLicao)
        {
            return (from r in _responsavelRepository.Queryable()
                    join e in _entregaDeLicaoRepository.Queryable() on r.IdEntregaDeLicao equals e.Id
                    where r.IdAluno == idAluno
                       && e.IdLicao == idLicao
                    select e).Any();
        }

        public IList<EntregaDeLicaoIniciadaDTO> ListarEntregasIniciadasPeloUsuarioNoCaseDeNegocio(int idCaseDeNegocio, int idUsuario)
        {
            return (from r in _responsavelRepository.Queryable()
                    join a in _alunoRepository.Queryable() on r.IdAluno equals a.Id
                    join e in _entregaDeLicaoRepository.Queryable() on r.IdEntregaDeLicao equals e.Id
                    where a.IdUsuario == idUsuario
                       && e.Licao.IdCase == idCaseDeNegocio
                    select new EntregaDeLicaoIniciadaDTO
                    {
                        IdEntregaDeLicao = e.Id,
                        IdLicao = e.Licao.Id,
                        Status = e.Status,
                        DataHoraEntrega = e.DataHoraEntrega
                    }).ToList();
        }

        public bool PermiteVisualizarLicao(Usuario usuario, IList<ResponsavelPelaLicaoDTO> responsaveis)
        {
            return responsaveis.Any(r => r.IdUsuario == usuario.Id);
        }

        public IList<ResponsavelPelaLicaoDTO> ListarResponsaveisPelaEntregaDeLicao(int idEntregaDeLicao)
        {
            return (from u in _usuarioRepository.Queryable()
                    join a in _alunoRepository.Queryable() on u.Id equals a.IdUsuario
                    join r in _responsavelRepository.Queryable() on a.Id equals r.IdAluno
                    where r.IdEntregaDeLicao == idEntregaDeLicao
                    select new ResponsavelPelaLicaoDTO
                    {
                        IdUsuario = u.Id,
                        Nome = u.NomeCompleto
                    }).ToList();
        }

        public bool UsuarioEhResponsavelPelaEntregaDeLicao(int idEntregaDeLicao, int idUsuario)
        {
            return (from a in _alunoRepository.Queryable()
                    join r in _responsavelRepository.Queryable() on a.Id equals r.IdAluno
                    where r.IdEntregaDeLicao == idEntregaDeLicao && a.IdUsuario == idUsuario
                    select a.IdUsuario).Any();
        }
    }
}