using Infra.Configurations;
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
            modelBuilder.ApplyConfiguration(new AlunoDoCaseConfiguration());
            modelBuilder.ApplyConfiguration(new CaseDeNegocioConfiguration());
            modelBuilder.ApplyConfiguration(new EntregaDeLicaoConfiguration());
            modelBuilder.ApplyConfiguration(new EntregaDeTrofeuConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
            modelBuilder.ApplyConfiguration(new LicaoConfiguration());
            modelBuilder.ApplyConfiguration(new MembroDoGrupoConfiguration());
            modelBuilder.ApplyConfiguration(new QuestaoConfiguration());
            modelBuilder.ApplyConfiguration(new ResponsavelPelaLicaoConfiguration());
            modelBuilder.ApplyConfiguration(new RespostaConfiguration());
            modelBuilder.ApplyConfiguration(new TrofeuConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
