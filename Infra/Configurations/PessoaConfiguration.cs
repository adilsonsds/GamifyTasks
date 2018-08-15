using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NomeCompleto)
                .HasMaxLength(200)
                .IsRequired();
            

        }
    }
}