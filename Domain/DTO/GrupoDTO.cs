using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.DTO
{
    public class GrupoDTO
    {
        public GrupoDTO()
        {

            //MembrosDoGrupo = new List<MembroDoGrupo>();
        }

        public GrupoDTO(Grupo grupo)
        {
            Id = grupo.Id;
            Nome = grupo.Nome;
            idCase = grupo.IdCase;
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public int idCase { get; set; }
        public void PreencherEntidade(Grupo grupo)
        {
            grupo.Nome = Nome;
        }
    }
}