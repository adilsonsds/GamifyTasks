using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_usuario");

            builder.Property(p => p.NomeCompleto)
                .HasColumnName("nome_completo")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasColumnName("senha")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.DataHoraCadastro)
                .HasColumnName("data_hora_cadastro")
                .HasColumnType("TIMESTAMP(0)")
                .IsRequired();
        }
    }
}