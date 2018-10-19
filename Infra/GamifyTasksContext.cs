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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CaseDeNegocioConfiguration());
            modelBuilder.ApplyConfiguration(new LicaoConfiguration());
            modelBuilder.ApplyConfiguration(new QuestaoConfiguration());
            modelBuilder.ApplyConfiguration(new TrofeuConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
