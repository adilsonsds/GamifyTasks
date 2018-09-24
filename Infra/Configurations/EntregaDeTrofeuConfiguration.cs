using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class EntregaDeTrofeuConfiguration : IEntityTypeConfiguration<EntregaDeTrofeu>
    {
        public void Configure(EntityTypeBuilder<EntregaDeTrofeu> builder)
        {
            builder.ToTable("EntregaDeTrofeu");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Trofeu)
                .HasColumnName("IdTrofeu")
                .IsRequired();

            builder.Property(p => p.EntregaDeLicao)
                .HasColumnName("IdEntregarLicao")
                .IsRequired();

            builder.Property(p => p.Resposta)
                .HasColumnName("IdResposta")
                .IsRequired();

        }
    }
}