using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class MembroDoGrupoConfiguration : IEntityTypeConfiguration<MembroDoGrupo>
    {
        public void Configure(EntityTypeBuilder<MembroDoGrupo> builder)
        {
<<<<<<< HEAD
            builder.ToTable("grupo_membro");
=======
            builder.ToTable("membro_do_grupo");
>>>>>>> 9b66947032748fab79a97b6a4605a3dfc9264fcf

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
<<<<<<< HEAD
                .HasColumnName("id_grupo_membro");
            
            builder.Property(p => p.IdGrupo)
                .HasColumnName("id_grupo")
                .IsRequired();

            builder.Property(p => p.IdAluno)
=======
                .HasColumnName("id_membro_do_grupo");

            builder.Property(p => p.Grupo)
                .HasColumnName("id_grupo")
                .IsRequired();

            builder.Property(p => p.Aluno)
>>>>>>> 9b66947032748fab79a97b6a4605a3dfc9264fcf
                .HasColumnName("id_case_negocio_aluno")
                .IsRequired();
        }
    }
}