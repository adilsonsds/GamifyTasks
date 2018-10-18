using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ILicaoService : IService<Licao>
    {
        int Adicionar(LicaoDTO licaoDTO);

        void Atualizar(LicaoDTO licaoDTO);

        IEnumerable<LicaoDTO> Listar(int idCaseDeNegocio, int? idLicao = null);
    }
}