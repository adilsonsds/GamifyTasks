using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class LicaoConfiguration : IEntityTypeConfiguration<Licao>
    {
        public void Configure(EntityTypeBuilder<Licao> builder)
        {
            builder.ToTable("licao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_licao");

            builder.Property(p => p.IdCase)
                .HasColumnName("id_case")
                .IsRequired();

            builder.Property(p => p.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.TextoApresentacao)
                .HasColumnName("texto_apresentacao")
                .HasMaxLength(10000);

            builder.Property(p => p.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.FormaDeEntrega)
                .HasColumnName("forma_de_entrega")
                .IsRequired();

            builder.Property(p => p.DataLiberacao)
                .HasColumnName("data_liberacao");

            builder.Property(p => p.DataEncerramento)
                .HasColumnName("data_encerramento");

            builder.Property(p => p.PermiteEntregasForaDoPrazo)
                .HasColumnName("permite_entregas_fora_do_prazo")
                .IsRequired();
        }
    }
}