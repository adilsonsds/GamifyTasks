namespace Domain.Entities
{
    public class ResponsavelPelaLicao
    {
        public int Id { get; set; }
        public EntregaDeLicao EntregaDeLicao { get; set; }
        public AlunoDoCase Aluno { get; set; }
    }
}