using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class ResponsavelPelaLicaoConfiguration : IEntityTypeConfiguration<ResponsavelPelaLicao>
    {
        public void Configure(EntityTypeBuilder<ResponsavelPelaLicao> builder)
        {
            builder.ToTable("licao_responsavel");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_licao_responsavel");

            builder.Property(p => p.IdEntregaDeLicao)
                .HasColumnName("id_licao_entrega")
                .IsRequired();

            builder.Property(p => p.IdAluno)
                .HasColumnName("id_case_negocio_aluno")
                .IsRequired();
        }
    }
}