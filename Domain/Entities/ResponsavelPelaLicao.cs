namespace Domain.Entities
{
    public class ResponsavelPelaLicao : TEntity
    {
        public ResponsavelPelaLicao()
        {

        }

        public ResponsavelPelaLicao(EntregaDeLicao entrega, AlunoDoCase alunoDoCase)
        {
            IdEntregaDeLicao = entrega.Id;
            IdAluno = alunoDoCase.Id;
        }

        public int IdEntregaDeLicao { get; set; }
        public int IdAluno { get; set; }
    }
}