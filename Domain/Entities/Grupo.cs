using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Grupo
    {
        public int Id { get; set; }
        public Case Case { get; set; }
        public string Nome { get; set; }
        public IList<MembroDoGrupo> Membros { get; set; }
    }
}