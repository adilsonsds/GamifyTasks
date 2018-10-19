using System.Collections.Generic;

namespace Domain.Entities
{
    public class Grupo : TEntity
    {
        public CaseDeNegocio Case { get; set; }
        public string Nome { get; set; }
        public IList<MembroDoGrupo> Membros { get; set; }
    }
}