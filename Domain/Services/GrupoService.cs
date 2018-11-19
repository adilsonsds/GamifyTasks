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
        private readonly IResponsavelPelaLicaoRepository _responsavelPelaLicaoRepository;
        private readonly IEntregaDeTrofeuRepository _entregaDeTrofeuRepository;
        private readonly ITrofeuRepository _trofeuRepository;
        private readonly IRespostaRepository _respostaRepository;

        public GrupoService(IGrupoRepository grupoRepository, ICaseDeNegocioRepository caseDeNegocioRepository,
            IUsuarioRepository usuarioRepository, IAlunoDoCaseRepository alunoRepository, IMembroDoGrupoRepository membroDoGrupoRepository,
            IEntregaDeLicaoRepository entregaDeLicaoRepository, IResponsavelPelaLicaoRepository responsavelPelaLicaoRepository,
            IEntregaDeTrofeuRepository entregaDeTrofeuRepository, ITrofeuRepository trofeuRepository, IRespostaRepository respostaRepository)
            : base(grupoRepository)
        {
            _grupoRepository = grupoRepository;
            _caseDeNegocioRepository = caseDeNegocioRepository;
            _usuarioRepository = usuarioRepository;
            _alunoRepository = alunoRepository;
            _membroDoGrupoRepository = membroDoGrupoRepository;
            _entregaDeLicaoRepository = entregaDeLicaoRepository;
            _responsavelPelaLicaoRepository = responsavelPelaLicaoRepository;
            _entregaDeTrofeuRepository = entregaDeTrofeuRepository;
            _trofeuRepository = trofeuRepository;
            _respostaRepository = respostaRepository;
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

            if (GrupoJaTeveLicaoEntregue(grupo.Id))
                throw new Exception("Não é permitido alterações em grupos que já tiveram lições entregues.");

            grupo.Nome = request.Nome;
            grupo.GritoDeGuerra = request.GritoDeGuerra;

            AtualizarListaDeMembros(grupo, request.IdsAlunosMembros);

            Atualizar(grupo);
        }

        public IEnumerable<GrupoDTO> ListarPorCaseDeNegocio(int idCaseDeNegocio)
        {
            var queryGrupos = _grupoRepository.Queryable().Where(g => g.IdCase == idCaseDeNegocio);

            var grupos = queryGrupos
                .Select(g => new GrupoDTO
                {
                    Id = g.Id,
                    Nome = g.Nome
                })
                .ToList();

            if (grupos.Any())
            {
                AplicarPontosGanhosComRespostas(grupos, queryGrupos);
                AplicarPontosGanhosComTrofeus(grupos, queryGrupos);
                grupos = OrdenarPelaQuantidadeDePontosDoMaiorParaMenor(grupos);
            }

            return grupos;
        }

        public GrupoDetalhesDTO ObterDetalhes(int idGrupo)
        {
            Grupo grupo = _grupoRepository.Queryable().FirstOrDefault(g => g.Id == idGrupo);
            var response = new GrupoDetalhesDTO(grupo);
            response.Membros = ListarMembros(grupo.Id);
            return response;
        }

        public IList<MembroDoGrupoDTO> PesquisarNovosMembros(int idCase, string nomeAluno)
        {
            var queryUsuarios = _usuarioRepository.Queryable();

            if (!string.IsNullOrWhiteSpace(nomeAluno))
                queryUsuarios = queryUsuarios.Where(u => u.NomeCompleto.Contains(nomeAluno));

            return (from u in queryUsuarios
                    join a in _alunoRepository.Queryable() on u.Id equals a.IdUsuario
                    where a.IdCaseDeNegocio == idCase
                       && !_membroDoGrupoRepository.Queryable().Any(m => m.IdAluno == a.Id)
                    orderby u.NomeCompleto
                    select new MembroDoGrupoDTO
                    {
                        IdUsuario = u.Id,
                        IdAluno = a.Id,
                        NomeCompleto = u.NomeCompleto
                    })
                    .Take(50)
                    .ToList();
        }

        public MontarGrupoDTO ObterDadosParaMontagemDeGrupos(Usuario usuarioLogado, int? idCase = null, int? idGrupo = null)
        {
            if (!idCase.HasValue && !idGrupo.HasValue)
                throw new Exception("Solicitação invélida.");

            var response = new MontarGrupoDTO();

            CaseDeNegocio caseDeNegocio = null;

            if (idGrupo.HasValue)
            {
                Grupo grupo = ObterPorId(idGrupo.Value);

                if (grupo == null)
                    throw new Exception("Grupo inexistente.");

                caseDeNegocio = grupo.CaseDeNegocio;

                response.IdGrupo = idGrupo.Value;
                response.NomeGrupo = grupo.Nome;
                response.GritoDeGuerra = grupo.GritoDeGuerra;
                response.PermiteAlterarMembros = GrupoJaTeveLicaoEntregue(idGrupo.Value);
                response.Membros = ListarMembros(idGrupo.Value);
            }
            else
            {
                caseDeNegocio = _caseDeNegocioRepository.GetById(idCase.Value);

                if (caseDeNegocio == null)
                    throw new Exception("Case de negócio não encontrado.");

                var alunoDoCase = _alunoRepository.Obter(usuarioLogado.Id, caseDeNegocio.Id);
                if (alunoDoCase != null)
                {
                    response.Membros.Add(new MembroDoGrupoDTO(usuarioLogado, alunoDoCase));
                }

                response.PermiteAlterarMembros = true;
            }

            if (!caseDeNegocio.PermiteMontarGrupos)
                throw new Exception("Case de negócio não permite grupos.");

            response.IdCase = caseDeNegocio.Id;
            response.MinimoPermitidoDeAlunos = caseDeNegocio.MinimoDeAlunosPorGrupo;
            response.MaximoPermitidoDeAlunos = caseDeNegocio.MaximoDeAlunosPorGrupo;

            return response;
        }


        #region Métodos privados
        private bool GrupoJaTeveLicaoEntregue(int idGrupo)
        {
            return (from e in _entregaDeLicaoRepository.Queryable()
                    where e.IdGrupo == idGrupo && e.Status == EntregaDeLicaoStatusEnum.Entregue
                    select e).Any();
        }

        private IList<MembroDoGrupoDTO> ListarMembros(int idGrupo)
        {
            return (from m in _membroDoGrupoRepository.Queryable()
                    join a in _alunoRepository.Queryable() on m.IdAluno equals a.Id
                    join u in _usuarioRepository.Queryable() on a.IdUsuario equals u.Id
                    where m.IdGrupo == idGrupo
                    select new MembroDoGrupoDTO
                    {
                        IdUsuario = u.Id,
                        IdAluno = a.Id,
                        NomeCompleto = u.NomeCompleto
                    }).ToList();
        }

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

        private List<GrupoDTO> OrdenarPelaQuantidadeDePontosDoMaiorParaMenor(List<GrupoDTO> grupos)
        {
            return grupos
                .OrderByDescending(a => a.Pontos)
                .ThenBy(a => a.Nome)
                .ToList();
        }

        private void AplicarPontosGanhosComRespostas(List<GrupoDTO> grupos, IQueryable<Grupo> queryGrupos)
        {
            var pontosDasRespostas = (from g in queryGrupos
                                      join e in _entregaDeLicaoRepository.Queryable() on g.Id equals e.IdGrupo
                                      join re in _respostaRepository.Queryable() on e.Id equals re.IdEntregaDeLicao
                                      where re.PontosGanhos != null
                                      group re by g.Id into g
                                      select new
                                      {
                                          IdGrupo = g.Key,
                                          Pontos = g.Sum(p => p.PontosGanhos.Value)
                                      }).ToList();

            foreach (var grupo in grupos)
            {
                var totalDasRespostas = pontosDasRespostas.FirstOrDefault(p => p.IdGrupo == grupo.Id);
                if (totalDasRespostas != null)
                    grupo.Pontos += totalDasRespostas.Pontos;
            }
        }

        private void AplicarPontosGanhosComTrofeus(List<GrupoDTO> grupos, IQueryable<Grupo> queryGrupos)
        {
            var pontosDosTrofeus = (from g in queryGrupos
                                    join el in _entregaDeLicaoRepository.Queryable() on g.Id equals el.IdGrupo
                                    join et in _entregaDeTrofeuRepository.Queryable() on el.Id equals et.IdEntregaDeLicao
                                    join t in _trofeuRepository.Queryable() on et.IdTrofeu equals t.Id
                                    group t by g.Id into g
                                    select new
                                    {
                                        IdGrupo = g.Key,
                                        Pontos = g.Sum(trofeu => trofeu.Pontos)
                                    }).ToList();

            foreach (var grupo in grupos)
            {
                var totalDosTrofeus = pontosDosTrofeus.FirstOrDefault(p => p.IdGrupo == grupo.Id);
                if (totalDosTrofeus != null)
                    grupo.Pontos += totalDosTrofeus.Pontos;
            }
        }
        #endregion
    }
}