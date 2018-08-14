using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();   // Create the database
        }
 
        // The Accounts will be the name of the table 
        public DbSet<Pessoa> Pessoas { get; set; }
 
        // Use this method, if you want to change the properties such as custom table names and others.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
 
        // This method will be used to configure the database properties such as Connection String(s) and more.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("{ConnectionString}");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
