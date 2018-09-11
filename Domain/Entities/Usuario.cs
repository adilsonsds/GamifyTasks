using System;

namespace Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            DataHoraCadastro = DateTime.Now;
        }

        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}