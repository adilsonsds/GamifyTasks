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

        public ConsultaDeAlunosService(IAlunoDoCaseRepository alunoDoCaseRepository, IMembroDoGrupoRepository membroDoGrupoRepository,
            IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository)
        {
            _membroDoGrupoRepository = membroDoGrupoRepository;
            _alunoDoCaseRepository = alunoDoCaseRepository;
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
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

            //TODO:review aplicar validação para obter grupos somente se o case permite formação de grupos
            if (alunos.Any())
                AplicarDadosDeGrupos(alunos, queryAlunos);

            return alunos;
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
    }
}