using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class AlunoDoCaseConfiguration : IEntityTypeConfiguration<AlunoDoCase>
    {
        public void Configure(EntityTypeBuilder<AlunoDoCase> builder)
        {
            builder.ToTable("case_negocio_aluno");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_case_negocio_aluno");

            builder.Property(p => p.IdCaseDeNegocio)
                .HasColumnName("id_case_negocio")
                .IsRequired();

            builder.Property(p => p.IdUsuario)
                .HasColumnName("id_usuario")
                .IsRequired();
        }
    }
}
