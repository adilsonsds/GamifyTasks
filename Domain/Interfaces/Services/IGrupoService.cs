using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Request;

namespace Domain.Interfaces.Services
{
    public interface IGrupoService : IService<Grupo>
    {
        int Adicionar(ManterGrupoRequest request);
        void Atualizar(ManterGrupoRequest request);
        IEnumerable<GrupoDTO> ListarPorCaseDeNegocio(int idCaseDeNegocio);
        GrupoDetalhesDTO ObterDetalhes(int idGrupo);

    }
}