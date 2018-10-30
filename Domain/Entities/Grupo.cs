using System.Collections.Generic;

namespace Domain.Entities
{
    public class Grupo : TEntity
    {
        public Grupo()
        {
            Membros = new List<MembroDoGrupo>();
        }

        public Grupo(CaseDeNegocio caseDeNegocio, string nome, string gritoDeGuerra) : this()
        {
            Nome = nome;
            GritoDeGuerra = gritoDeGuerra;
            IdCase = caseDeNegocio.Id;
            CaseDeNegocio = caseDeNegocio;
        }

        public int IdCase { get; set; }
        public string Nome { get; set; }
        public string GritoDeGuerra { get; set; }
        public virtual CaseDeNegocio CaseDeNegocio { get; set; }
        public virtual ICollection<MembroDoGrupo> Membros { get; set; }
    }
}