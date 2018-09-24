// using Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infra.Configurations
// {
//     public class AlunoDoCaseConfiguration : IEntityTypeConfiguration<AlunoDoCase>
//     {
//         public void Configure(EntityTypeBuilder<AlunoDoCase> builder)
//         {
//             builder.ToTable("Licao");

//             builder.HasKey(p => p.Id);

//             builder.Property(p => p.Id)
//                 .HasColumnName("Id");

//             builder.Property(p => p.Case)
//                 .HasColumnName("");

//             builder.Property(p => p.NomeCompleto)
//                 .HasColumnName("IdCase");

//             builder.Property(p => p.Email)
//                 .HasColumnName("Email")
//                 .HasMaxLength(100)
//                 .IsRequired();

//             builder.Property(p => p.Senha)
//                 .HasColumnName("Senha")
//                 .HasMaxLength(50)
//                 .IsRequired();

//             builder.Property(p => p.DataHoraCadastro)
//                 .HasColumnName("DataHoraCadastro")
//                 .HasColumnType("TIMESTAMP(0)")
//                 .IsRequired();
//         }
//     }
// }