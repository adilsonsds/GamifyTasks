using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Conta");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.DataHoraCadastro)
                .HasColumnType("TIMESTAMP(0)")
                .IsRequired();
        }
    }
}