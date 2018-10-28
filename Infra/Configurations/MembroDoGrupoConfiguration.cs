using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class MembroDoGrupoConfiguration : IEntityTypeConfiguration<MembroDoGrupo>
    {
        public void Configure(EntityTypeBuilder<MembroDoGrupo> builder)
        {
            builder.ToTable("membro_do_grupo");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_membro_do_grupo");

            builder.Property(p => p.Grupo)
                .HasColumnName("id_grupo")
                .IsRequired();

            builder.Property(p => p.Aluno)
                .HasColumnName("id_case_negocio_aluno")
                .IsRequired();
        }
    }
}