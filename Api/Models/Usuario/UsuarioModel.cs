namespace Api.Models.Usuario
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public void PreencherEntidade(Domain.Entities.Usuario usuario)
        {
            usuario.NomeCompleto = $"{Nome} {Sobrenome}";
            usuario.Email = Email;
            usuario.Senha = Senha;
        }
    }
}