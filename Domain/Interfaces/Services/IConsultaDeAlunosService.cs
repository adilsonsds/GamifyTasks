using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IConsultaDeAlunosService : IService
    {
        AlunoDoCase ObterAlunoInscritoNoCase(int idUsuario, int IdCaseDeNegocio);

        IList<AlunoDoCase> ListarAlunosQueSejamMembrosNoGrupo(int idGrupo);

        IList<AlunoDoCase> ListarOutrosAlunosQueSejamMembrosNoGrupo(int idGrupo, int idAlunoQueJaEhMembro);

        int? ObterIdGrupoOndeUsuarioEstejaParticipando(int idUsuario, int idCase);

        bool UsuarioFazParteDeAlgumGrupoDoCaseDeNegocio(int idUsuario, int idCase);

        IList<AlunoDoCaseDTO> ListarAlunosPorCase(int idCaseDeNegocio);

        IEnumerable<CaseDoAlunoDTO> ListarCasesOndeUsuarioPossuiInscricao(int idUsuario);

        IEnumerable<GrupoDoAlunoDTO> ListarGruposOndeUsuarioEhMembro(int idUsuario);
        
    }
}