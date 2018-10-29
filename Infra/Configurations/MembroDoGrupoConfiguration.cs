using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class MembroDoGrupoConfiguration : IEntityTypeConfiguration<MembroDoGrupo>
    {
        public void Configure(EntityTypeBuilder<MembroDoGrupo> builder)
        {
            builder.ToTable("grupo_membro");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_grupo_membro");
            
            builder.Property(p => p.IdGrupo)
                .HasColumnName("id_grupo")
                .IsRequired();

            builder.Property(p => p.IdAluno)
                .HasColumnName("id_case_negocio_aluno")
                .IsRequired();
        }
    }
}