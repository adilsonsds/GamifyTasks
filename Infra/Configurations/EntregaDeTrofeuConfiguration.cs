using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class EntregaDeTrofeuConfiguration : IEntityTypeConfiguration<EntregaDeTrofeu>
    {
        public void Configure(EntityTypeBuilder<EntregaDeTrofeu> builder)
        {
            builder.ToTable("trofeu_entrega");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_trofeu_entrega");

            builder.Property(p => p.IdTrofeu)
                .HasColumnName("id_trofeu")
                .IsRequired();

            builder.Property(p => p.IdEntregaDeLicao)
                .HasColumnName("id_licao_entrega")
                .IsRequired();

            builder.Property(p => p.IdResposta)
                .HasColumnName("id_resposta");

        }
    }
}