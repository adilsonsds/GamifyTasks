using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable("grupo");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_grupo");

            builder.Property(p => p.Nome)
                .HasColumnName("nome");

            builder.HasOne(p => p.CaseDeNegocio)
                .WithMany()
                .HasForeignKey(p => p.IdCase);

        }
    }
}