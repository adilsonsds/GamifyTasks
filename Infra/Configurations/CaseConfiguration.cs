using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Case");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Professor)
                .HasColumnName("IdUsuarioProfessor")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(p => p.TextoDeApresentacao)
                .HasColumnName("TextoDeApresentacao");

            builder.Property(p => p.PermiteMontarGrupos)
                .HasColumnName("PermiteMontarGrupos")
                .IsRequired();

            builder.Property(p => p.MinimoDeAlunosPorGrupo)
                .HasColumnName("MinimoDeAlunosPorGrupo")
                .IsRequired();
            
            builder.Property(p => p.MinimoDeAlunosPorGrupo)
                .HasColumnName("MaximoDeAlunosPorGrupo")
                .IsRequired();
        }
    }
}