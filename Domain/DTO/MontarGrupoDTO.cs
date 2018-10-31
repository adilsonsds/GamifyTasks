using System.Collections.Generic;

namespace Domain.DTO
{
    public class MontarGrupoDTO
    {
        public MontarGrupoDTO()
        {
            Membros = new List<MembroDoGrupoDTO>();
        }

        public int IdCase { get; set; }
        public int? IdGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public string GritoDeGuerra { get; set; }
        public int? MinimoPermitidoDeAlunos { get; set; }
        public int? MaximoPermitidoDeAlunos { get; set; }
        public bool PermiteAlterarMembros { get; set; }
        public IList<MembroDoGrupoDTO> Membros { get; set; }
    }
}