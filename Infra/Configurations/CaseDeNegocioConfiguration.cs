using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class CaseDeNegocioConfiguration : IEntityTypeConfiguration<CaseDeNegocio>
    {
        public void Configure(EntityTypeBuilder<CaseDeNegocio> builder)
        {
            builder.ToTable("case_negocio");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_case_negocio");

            // builder.HasOne(p => p.Professor)
            //     .WithMany()
            //     .HasForeignKey(p => p.IdProfessor);

            builder.Property(p => p.IdProfessor)
                .HasColumnName("id_professor")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.TextoDeApresentacao)
                .HasColumnName("texto_apresentacao")
                .HasMaxLength(5000)
                .IsRequired();

            builder.Property(p => p.PermiteMontarGrupos)
                .HasColumnName("permite_montar_grupos")
                .IsRequired();

            builder.Property(p => p.MinimoDeAlunosPorGrupo)
                .HasColumnName("minimo_alunos_por_grupo");

            builder.Property(p => p.MaximoDeAlunosPorGrupo)
                .HasColumnName("maximo_alunos_por_grupo");
        }
    }
}