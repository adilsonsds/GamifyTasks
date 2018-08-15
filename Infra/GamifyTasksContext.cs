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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaConfiguration());
            // modelBuilder.ApplyConfiguration(new PessoaConfiguration());

        }
    }
}
