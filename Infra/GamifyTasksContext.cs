using System;
using Infra.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class GamifyTasksContext : DbContext
    {
        public GamifyTasksContext(DbContextOptions<GamifyTasksContext> options)
            : base(options)
        {

        }

        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaConfiguration());
            // modelBuilder.ApplyConfiguration(new PessoaConfiguration());

            // base.OnModelCreating(modelBuilder);
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql("Host=localhost;Database=gamifytasks;Username=postgres;Password=123456");
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}
