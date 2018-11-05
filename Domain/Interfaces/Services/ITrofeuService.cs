using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ITrofeuService : IService<Trofeu>
    {
        int Adicionar(TrofeuDTO trofeuDTO, Usuario usuarioLogado);

        void Atualizar(TrofeuDTO trofeuDTO, Usuario usuarioLogado);

        IEnumerable<TrofeuDTO> Listar(int idCaseDeNegocio);

        TrofeuDTO Obter(int idCaseDeNegocio, int idTrofeu);

    }
}