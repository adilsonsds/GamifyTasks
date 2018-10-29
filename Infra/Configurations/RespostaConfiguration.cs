using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class RespostaConfiguration : IEntityTypeConfiguration<Resposta>
    {
        public void Configure(EntityTypeBuilder<Resposta> builder)
        {
            builder.ToTable("resposta");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_resposta");

            builder.Property(p => p.IdEntregaDeLicao)
                .HasColumnName("id_licao_entrega");

            builder.Property(p => p.IdQuestao)
                .HasColumnName("id_questao");

            builder.Property(p => p.Conteudo)
                .HasColumnName("conteudo")
                .HasMaxLength(10000)
                .IsRequired();

            builder.Property(p => p.PontosGanhos)
                .HasColumnName("pontos_ganhos");
        }
    }
}