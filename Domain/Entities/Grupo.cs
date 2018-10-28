using System.Collections.Generic;

namespace Domain.Entities
{
    public class Grupo : TEntity
    {
        public int IdCase { get; set; }
        public virtual CaseDeNegocio CaseDeNegocio { get; set; }
        public string Nome { get; set; }
        
    }
}