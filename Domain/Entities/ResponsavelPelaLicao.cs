namespace Domain.Entities
{
    public class ResponsavelPelaLicao : TEntity
    {
        public EntregaDeLicao EntregaDeLicao { get; set; }
        public AlunoDoCase Aluno { get; set; }
    }
}