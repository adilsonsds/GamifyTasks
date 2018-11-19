namespace Domain.DTO
{
    public class GrupoDoAlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CaseDoAlunoDTO CaseDeNegocio { get; set; }
    }
}