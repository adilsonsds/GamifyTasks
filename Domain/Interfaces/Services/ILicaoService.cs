using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ILicaoService : IService<Licao>
    {
        int Adicionar(LicaoDTO licaoDTO, Usuario usuarioLogado);

        void Atualizar(LicaoDTO licaoDTO, Usuario usuarioLogado);

        IEnumerable<LicaoDTO> Listar(int idCaseDeNegocio, Usuario usuarioLogado);

        LicaoDTO Obter(int idCaseDeNegocio, int idLicao, Usuario usuarioLogado);

        LicaoDTO ObterEntrega(int IdEntregaDeLicao, Usuario usuarioLogado);
        
    }
}