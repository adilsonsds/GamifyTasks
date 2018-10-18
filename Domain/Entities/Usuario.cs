using System;

namespace Domain.Entities
{
    public class Usuario : TEntity
    {
        public Usuario()
        {
            DataHoraCadastro = DateTime.Now;
        }

        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}