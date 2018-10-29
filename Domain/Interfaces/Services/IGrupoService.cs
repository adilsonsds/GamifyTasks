using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IGrupoService : IService<Grupo>
    {
        int Adicionar(GrupoDTO grupoDTO);
        void Atualizar(GrupoDTO grupoDTO);
        IEnumerable<GrupoDTO> Listar(int idCaseDeNegocio,int? idGrupo = null);
        GrupoDTO ObterPorId(int idCase, int idGrupo);

    }
}