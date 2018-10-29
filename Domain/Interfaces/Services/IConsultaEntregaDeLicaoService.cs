using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IConsultaEntregaDeLicaoService : IService
    {
        bool AlunoJaComecouAFazerALicao(int idAluno, int idLicao);

        IList<EntregaDeLicaoIniciadaDTO> ListarEntregasIniciadasPeloUsuarioNoCaseDeNegocio(int idCaseDeNegocio, int idUsuario);

        bool PermiteVisualizarLicao(Usuario usuario, IList<ResponsavelPelaLicaoDTO> responsaveis);

        IList<ResponsavelPelaLicaoDTO> ListarResponsaveisPelaEntregaDeLicao(int idEntregaDeLicao);

        bool UsuarioEhResponsavelPelaEntregaDeLicao(int idEntregaDeLicao, int idUsuario);

    }
}