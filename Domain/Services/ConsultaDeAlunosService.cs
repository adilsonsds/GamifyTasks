using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ConsultaDeAlunosService : BaseService, IConsultaDeAlunosService
    {
        private readonly IAlunoDoCaseRepository _alunoDoCaseRepository;
        private readonly IMembroDoGrupoRepository _membroDoGrupoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly ICaseDeNegocioRepository _caseDeNegocioRepository;
        private readonly IRespostaRepository _respostaRepository;
        private readonly IResponsavelPelaLicaoRepository _responsavelPelaLicaoRepository;
        private readonly IEntregaDeTrofeuRepository _entregaDeTrofeuRepository;
        private readonly ITrofeuRepository _trofeuRepository;

        public ConsultaDeAlunosService(IAlunoDoCaseRepository alunoDoCaseRepository, IMembroDoGrupoRepository membroDoGrupoRepository,
            IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository, ICaseDeNegocioRepository caseDeNegocioRepository,
            IRespostaRepository respostaRepository, IResponsavelPelaLicaoRepository responsavelPelaLicaoRepository,
            IEntregaDeTrofeuRepository entregaDeTrofeuRepository, ITrofeuRepository trofeuRepository)
        {
            _membroDoGrupoRepository = membroDoGrupoRepository;
            _alunoDoCaseRepository = alunoDoCaseRepository;
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _caseDeNegocioRepository = caseDeNegocioRepository;
            _respostaRepository = respostaRepository;
            _responsavelPelaLicaoRepository = responsavelPelaLicaoRepository;
            _entregaDeTrofeuRepository = entregaDeTrofeuRepository;
            _trofeuRepository = trofeuRepository;
        }

        public AlunoDoCase ObterAlunoInscritoNoCase(int idUsuario, int IdCaseDeNegocio)
        {
            return _alunoDoCaseRepository.Obter(idUsuario, IdCaseDeNegocio);
        }

        public IList<AlunoDoCase> ListarAlunosQueSejamMembrosNoGrupo(int idGrupo)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                    where m.IdGrupo == idGrupo
                    select a).ToList();
        }

        public IList<AlunoDoCase> ListarOutrosAlunosQueSejamMembrosNoGrupo(int idGrupo, int idAlunoQueJaEhMembro)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                    where m.IdGrupo == idGrupo && a.Id != idAlunoQueJaEhMembro
                    select a).ToList();
        }

        public int? ObterIdGrupoOndeUsuarioEstejaParticipando(int idUsuario, int idCase)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                    where a.IdUsuario == idUsuario
                       && a.IdCaseDeNegocio == idCase
                    select (int?)m.IdGrupo).FirstOrDefault();
        }

        public bool UsuarioFazParteDeAlgumGrupoDoCaseDeNegocio(int idUsuario, int idCase)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                    where a.IdUsuario == idUsuario
                       && a.IdCaseDeNegocio == idCase
                    select m.IdGrupo).Any();
        }

        public IList<AlunoDoCaseDTO> ListarAlunosPorCase(int idCaseDeNegocio)
        {
            var queryAlunos = _alunoDoCaseRepository.Queryable().Where(a => a.IdCaseDeNegocio == idCaseDeNegocio);

            var alunos = (from a in queryAlunos
                          join u in _usuarioRepository.Queryable() on a.IdUsuario equals u.Id
                          select new AlunoDoCaseDTO
                          {
                              IdUsuario = u.Id,
                              NomeCompleto = u.NomeCompleto
                          }).ToList();

            if (alunos.Any())
            {
                AplicarPontosGanhosComRespostas(alunos, queryAlunos);
                AplicarPontosGanhosComTrofeus(alunos, queryAlunos);

                //TODO:review aplicar validação para obter grupos somente se o case permite formação de grupos
                AplicarDadosDeGrupos(alunos, queryAlunos);

                alunos = OrdenarPelaQuantidadeDePontosDoMarorParaMenor(alunos);
            }

            return alunos;
        }

        private List<AlunoDoCaseDTO> OrdenarPelaQuantidadeDePontosDoMarorParaMenor(List<AlunoDoCaseDTO> alunos)
        {
            return alunos
                .OrderByDescending(a => a.Pontos)
                .ThenBy(a => a.NomeCompleto)
                .ToList();
        }

        private void AplicarPontosGanhosComRespostas(List<AlunoDoCaseDTO> alunos, IQueryable<AlunoDoCase> queryAlunos)
        {
            var pontosDasRespostas = (from a in queryAlunos
                                      join rl in _responsavelPelaLicaoRepository.Queryable() on a.Id equals rl.IdAluno
                                      join re in _respostaRepository.Queryable() on rl.IdEntregaDeLicao equals re.IdEntregaDeLicao
                                      where re.PontosGanhos != null
                                      group re by a.IdUsuario into g
                                      select new
                                      {
                                          IdUsuario = g.Key,
                                          Pontos = g.Sum(p => p.PontosGanhos.Value)
                                      }).ToList();

            foreach (var aluno in alunos)
            {
                var totalDasRespostas = pontosDasRespostas.FirstOrDefault(p => p.IdUsuario == aluno.IdUsuario);
                if (totalDasRespostas != null)
                    aluno.Pontos += totalDasRespostas.Pontos;
            }
        }

        private void AplicarPontosGanhosComTrofeus(List<AlunoDoCaseDTO> alunos, IQueryable<AlunoDoCase> queryAlunos)
        {
            var pontosDosTrofeus = (from a in queryAlunos
                                    join rl in _responsavelPelaLicaoRepository.Queryable() on a.Id equals rl.IdAluno
                                    join et in _entregaDeTrofeuRepository.Queryable() on rl.IdEntregaDeLicao equals et.IdEntregaDeLicao
                                    join t in _trofeuRepository.Queryable() on et.IdTrofeu equals t.Id
                                    group t by a.IdUsuario into g
                                    select new
                                    {
                                        IdUsuario = g.Key,
                                        Pontos = g.Sum(trofeu => trofeu.Pontos)
                                    }).ToList();

            foreach (var aluno in alunos)
            {
                var totalDosTrofeus = pontosDosTrofeus.FirstOrDefault(p => p.IdUsuario == aluno.IdUsuario);
                if (totalDosTrofeus != null)
                    aluno.Pontos += totalDosTrofeus.Pontos;
            }
        }

        private void AplicarDadosDeGrupos(List<AlunoDoCaseDTO> alunos, IQueryable<AlunoDoCase> queryAlunos)
        {
            var membrosDeGrupos = (from a in queryAlunos
                                   join u in _usuarioRepository.Queryable() on a.IdUsuario equals u.Id
                                   join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                                   join g in _grupoRepository.Queryable() on m.IdGrupo equals g.Id
                                   select new
                                   {
                                       IdUsuario = u.Id,
                                       IdGrupo = g.Id,
                                       NomeGrupo = g.Nome
                                   }).ToList();

            foreach (var aluno in alunos)
            {
                var grupoDoAluno = membrosDeGrupos.FirstOrDefault(m => m.IdUsuario == aluno.IdUsuario);
                if (grupoDoAluno != null)
                {
                    aluno.IdGrupo = grupoDoAluno.IdGrupo;
                    aluno.NomeGrupo = grupoDoAluno.NomeGrupo;
                }
            }
        }

        public IEnumerable<CaseDoAlunoDTO> ListarCasesOndeUsuarioPossuiInscricao(int idUsuario)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join c in _caseDeNegocioRepository.Queryable() on a.IdCaseDeNegocio equals c.Id
                    where a.IdUsuario == idUsuario
                    select new CaseDoAlunoDTO
                    {
                        Id = a.IdCaseDeNegocio,
                        Nome = c.Nome
                    }).ToList();
        }

        public IEnumerable<GrupoDoAlunoDTO> ListarGruposOndeUsuarioEhMembro(int idUsuario)
        {
            return (from a in _alunoDoCaseRepository.Queryable()
                    join m in _membroDoGrupoRepository.Queryable() on a.Id equals m.IdAluno
                    join g in _grupoRepository.Queryable() on m.IdGrupo equals g.Id
                    join c in _caseDeNegocioRepository.Queryable() on g.IdCase equals c.Id
                    where a.IdUsuario == idUsuario
                    select new GrupoDoAlunoDTO
                    {
                        Id = m.IdGrupo,
                        Nome = g.Nome,
                        CaseDeNegocio = new CaseDoAlunoDTO
                        {
                            Id = c.Id,
                            Nome = c.Nome,
                        }
                    }).ToList();
        }
    }
}