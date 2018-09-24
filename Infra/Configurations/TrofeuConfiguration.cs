using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class TrofeuConfiguration : IEntityTypeConfiguration<Trofeu>
    {
        public void Configure(EntityTypeBuilder<Trofeu> builder)
        {
            builder.ToTable("trofeu");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_trofeu");

            builder.Property(p => p.IdCase)
                .HasColumnName("id_case_negocio")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.Pontos)
                .HasColumnName("pontos")
                .IsRequired();
        }
    }
}