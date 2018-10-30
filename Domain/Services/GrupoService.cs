using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Request;

namespace Domain.Services
{
    public class GrupoService : BaseService<Grupo>, IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly ICaseDeNegocioRepository _caseDeNegocioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAlunoDoCaseRepository _alunoRepository;
        private readonly IMembroDoGrupoRepository _membroDoGrupoRepository;
        private readonly IEntregaDeLicaoRepository _entregaDeLicaoRepository;

        public GrupoService(IGrupoRepository grupoRepository, ICaseDeNegocioRepository caseDeNegocioRepository,
            IUsuarioRepository usuarioRepository, IAlunoDoCaseRepository alunoRepository, IMembroDoGrupoRepository membroDoGrupoRepository,
            IEntregaDeLicaoRepository entregaDeLicaoRepository)
            : base(grupoRepository)
        {
            _grupoRepository = grupoRepository;
            _caseDeNegocioRepository = caseDeNegocioRepository;
            _usuarioRepository = usuarioRepository;
            _alunoRepository = alunoRepository;
            _membroDoGrupoRepository = membroDoGrupoRepository;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
        }

        public int Adicionar(ManterGrupoRequest request)
        {
            if (request == null || request.IdGrupo.HasValue)
                throw new Exception("Solicitação inválida.");

            if (request.IdsAlunosMembros == null || request.IdsAlunosMembros.Count < 2)
                throw new Exception("Um grupo deve ser formado por 2 ou mais alunos.");

            CaseDeNegocio caseDeNegocio = _caseDeNegocioRepository.GetById(request.IdCase);

            if (caseDeNegocio == null)
                throw new Exception("Case de negócio não encontrado.");

            if (!caseDeNegocio.PermiteMontarGrupos)
                throw new Exception("Case de negócio não permite criação de grupos.");

            if (request.IdsAlunosMembros.Count < caseDeNegocio.MinimoDeAlunosPorGrupo
             || request.IdsAlunosMembros.Count > caseDeNegocio.MaximoDeAlunosPorGrupo)
                throw new Exception(string.Format("Este case de negócio requer que o grupo seja formado com {0} a {1} alunos.", caseDeNegocio.MinimoDeAlunosPorGrupo, caseDeNegocio.MaximoDeAlunosPorGrupo));

            var alunos = (from a in _alunoRepository.Queryable()
                          where a.IdCaseDeNegocio == caseDeNegocio.Id
                             && request.IdsAlunosMembros.Contains(a.Id)
                          select a).ToList();

            if (!request.IdsAlunosMembros.All(id => alunos.Any(a => a.Id == id)))
                throw new Exception("Foi solicitado inclusão de aluno inexistente.");

            bool possuiAlunoQueJaEstaEmAlgumGrupo = (from m in _membroDoGrupoRepository.Queryable()
                                                     join a in _alunoRepository.Queryable() on m.IdAluno equals a.Id
                                                     where request.IdsAlunosMembros.Contains(m.IdAluno) && a.IdCaseDeNegocio == caseDeNegocio.Id
                                                     select m).Any();

            if (possuiAlunoQueJaEstaEmAlgumGrupo)
                throw new Exception("Foi solicitado inclusão de aluno que já possui grupo.");

            Grupo grupo = new Grupo(caseDeNegocio, request.Nome, request.GritoDeGuerra);

            foreach (var aluno in alunos)
                grupo.Membros.Add(new MembroDoGrupo(grupo, aluno));

            Adicionar(grupo);

            return grupo.Id;
        }

        public void Atualizar(ManterGrupoRequest request)
        {
            if (request == null || !request.IdGrupo.HasValue)
                throw new Exception("Solicitação inválida.");

            if (request.IdsAlunosMembros == null || request.IdsAlunosMembros.Count < 2)
                throw new Exception("Um grupo deve ser formado por 2 ou mais alunos.");

            Grupo grupo = ObterPorId(request.IdGrupo.Value);

            if (grupo == null)
                throw new Exception("Grupo não encontrado.");

            CaseDeNegocio caseDeNegocio = grupo.CaseDeNegocio;

            if (caseDeNegocio.PermiteMontarGrupos &&
                (request.IdsAlunosMembros.Count < caseDeNegocio.MinimoDeAlunosPorGrupo
                 || request.IdsAlunosMembros.Count > caseDeNegocio.MaximoDeAlunosPorGrupo))
                throw new Exception(string.Format("Este case de negócio requer que o grupo seja formado com {0} a {1} alunos.", caseDeNegocio.MinimoDeAlunosPorGrupo, caseDeNegocio.MaximoDeAlunosPorGrupo));

            int totalAlunosInscritos = (from a in _alunoRepository.Queryable()
                                        where a.IdCaseDeNegocio == caseDeNegocio.Id
                                           && request.IdsAlunosMembros.Contains(a.Id)
                                        select a).Count();

            if (totalAlunosInscritos != request.IdsAlunosMembros.Count)
                throw new Exception("Foi solicitado inclusão de aluno inexistente.");

            bool possuiAlunoQueJaEstaEmOutroGrupo = (from m in _membroDoGrupoRepository.Queryable()
                                                     join a in _alunoRepository.Queryable() on m.IdAluno equals a.Id
                                                     where request.IdsAlunosMembros.Contains(m.IdAluno)
                                                        && a.IdCaseDeNegocio == caseDeNegocio.Id
                                                        && m.IdGrupo != grupo.Id
                                                     select m).Any();

            if (possuiAlunoQueJaEstaEmOutroGrupo)
                throw new Exception("Foi solicitado inclusão de aluno que já possui grupo.");


            bool grupoJaTeveEntregaDeLicao = (from e in _entregaDeLicaoRepository.Queryable()
                                              where e.IdGrupo == grupo.Id && e.Status == EntregaDeLicaoStatusEnum.Entregue
                                              select e).Any();

            if (grupoJaTeveEntregaDeLicao)
                throw new Exception("Não é permitido alterações em grupos que já tiveram lições entregues.");

            grupo.Nome = request.Nome;
            grupo.GritoDeGuerra = request.GritoDeGuerra;

            AtualizarListaDeMembros(grupo, request.IdsAlunosMembros);

            Atualizar(grupo);
        }

        public IEnumerable<GrupoDTO> ListarPorCaseDeNegocio(int idCaseDeNegocio)
        {
            return _grupoRepository.Queryable()
                .Where(g => g.IdCase == idCaseDeNegocio)
                .Select(g => new GrupoDTO
                {
                    Id = g.Id,
                    Nome = g.Nome
                })
                .ToList();
        }

        public GrupoDetalhesDTO ObterDetalhes(int idGrupo)
        {
            Grupo grupo = _grupoRepository.Queryable().FirstOrDefault(g => g.Id == idGrupo);
            var response = new GrupoDetalhesDTO(grupo);

            response.Membros = (from m in _membroDoGrupoRepository.Queryable()
                                join a in _alunoRepository.Queryable() on m.IdAluno equals a.Id
                                join u in _usuarioRepository.Queryable() on a.IdUsuario equals u.Id
                                where m.IdGrupo == grupo.Id
                                select new MembroDoGrupoDTO
                                {
                                    IdUsuario = u.Id,
                                    IdAluno = a.Id,
                                    NomeCompleto = u.NomeCompleto
                                }).ToList();

            return response;
        }

        #region Métodos privados
        private void AtualizarListaDeMembros(Grupo grupo, IEnumerable<int> idsAlunosMembrosAdicionados)
        {
            foreach (int idAluno in idsAlunosMembrosAdicionados)
            {
                MembroDoGrupo membro = grupo.Membros.FirstOrDefault(m => m.IdAluno == idAluno);
                if (membro == null)
                {
                    membro = new MembroDoGrupo
                    {
                        IdAluno = idAluno,
                        IdGrupo = grupo.Id
                    };

                    grupo.Membros.Add(membro);
                }
            }

            List<MembroDoGrupo> membrosRemovidos = grupo.Membros.Where(qe => qe.Id > 0 && !idsAlunosMembrosAdicionados.Any(id => id == qe.IdAluno)).ToList();
            foreach (MembroDoGrupo membroRemovido in membrosRemovidos)
            {
                grupo.Membros.Remove(membroRemovido);
            }
        }
        #endregion
    }
}