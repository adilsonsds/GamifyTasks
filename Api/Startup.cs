using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infra;
using Domain.Repositories;
using Infra.Repositories;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();

            services.AddDbContext<GamifyTasksContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnectionString")
                )
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAlunoDoCaseRepository, AlunoDoCaseRepository>();
            services.AddScoped<ICaseDeNegocioRepository, CaseDeNegocioRepository>();
            services.AddScoped<IEntregaDeLicaoRepository, EntregaDeLicaoRepository>();
            services.AddScoped<IEntregaDeTrofeuRepository, EntregaDeTrofeuRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<ILicaoRepository, LicaoRepository>();
            services.AddScoped<IMembroDoGrupoRepository, MembroDoGrupoRepository>();
            services.AddScoped<IQuestaoRepository, QuestaoRepository>();
            services.AddScoped<IResponsavelPelaLicaoRepository, ResponsavelPelaLicaoRepository>();
            services.AddScoped<IRespostaRepository, RespostaRepository>();
            services.AddScoped<ITrofeuRepository, TrofeuRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => 
                builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
