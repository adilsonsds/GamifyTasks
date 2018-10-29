using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class EntregaDeLicaoConfiguration : IEntityTypeConfiguration<EntregaDeLicao>
    {
        public void Configure(EntityTypeBuilder<EntregaDeLicao> builder)
        {
            builder.ToTable("licao_entrega");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_licao_entrega");

            builder.Property(p => p.IdLicao)
                .HasColumnName("id_licao");

            builder.HasOne(p => p.Licao)
                .WithMany()
                .HasForeignKey(p => p.IdLicao);

            builder.Property(p => p.IdGrupo)
                .HasColumnName("id_grupo");

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(p => p.DataHoraEntrega)
                .HasColumnName("data_hora_entrega")
                .HasColumnType("TIMESTAMP(0)");

            builder.HasMany(p => p.Responsaveis)
                .WithOne()
                .HasForeignKey(p => p.IdEntregaDeLicao);

            builder.HasMany(p => p.Respostas)
                .WithOne()
                .HasForeignKey(p => p.IdEntregaDeLicao);
        }
    }
}