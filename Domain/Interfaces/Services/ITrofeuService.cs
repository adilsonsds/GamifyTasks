using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ITrofeuService : IService<Trofeu>
    {
        int Adicionar(TrofeuDTO trofeuDTO);

        void Atualizar(TrofeuDTO trofeuDTO);

        IEnumerable<TrofeuDTO> Listar(int idCaseDeNegocio, int? idTrofeu = null);

    }
}