using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class QuestaoConfiguration : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.ToTable("questao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_questao");

            builder.Property(p => p.IdLicao)
                .HasColumnName("id_licao");

            builder.Property(p => p.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(5000)
                .IsRequired();

            builder.Property(p => p.NotaMaxima)
                .HasColumnName("nota_maxima")
                .IsRequired();
        }
    }
}