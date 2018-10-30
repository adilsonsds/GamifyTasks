using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.DTO
{
    public class GrupoDetalhesDTO
    {
        public GrupoDetalhesDTO()
        {

        }

        public GrupoDetalhesDTO(Grupo grupo)
        {
            Id = grupo.Id;
            Nome = grupo.Nome;
            GritoDeGuerra = grupo.GritoDeGuerra;
            IdCase = grupo.IdCase;
            NomeCase = grupo.CaseDeNegocio.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string GritoDeGuerra { get; set; }
        public int IdCase { get; set; }
        public string NomeCase { get; set; }
        public IList<MembroDoGrupoDTO> Membros { get; set; }
    }
}