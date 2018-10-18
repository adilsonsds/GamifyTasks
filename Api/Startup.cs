using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infra;
using Domain.Interfaces.Repositories;
using Infra.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;

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

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAlunoDoCaseRepository, AlunoDoCaseRepository>();
            services.AddTransient<ICaseDeNegocioRepository, CaseDeNegocioRepository>();
            services.AddTransient<IEntregaDeLicaoRepository, EntregaDeLicaoRepository>();
            services.AddTransient<IEntregaDeTrofeuRepository, EntregaDeTrofeuRepository>();
            services.AddTransient<IGrupoRepository, GrupoRepository>();
            services.AddTransient<ILicaoRepository, LicaoRepository>();
            services.AddTransient<IMembroDoGrupoRepository, MembroDoGrupoRepository>();
            services.AddTransient<IQuestaoRepository, QuestaoRepository>();
            services.AddTransient<IResponsavelPelaLicaoRepository, ResponsavelPelaLicaoRepository>();
            services.AddTransient<IRespostaRepository, RespostaRepository>();
            services.AddTransient<ITrofeuRepository, TrofeuRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient(typeof(IService<>), typeof(BaseService<>));
            services.AddTransient<ICaseDeNegocioService, CaseDeNegocioService>();
            services.AddTransient<ILicaoService, LicaoService>();
            services.AddTransient<IQuestaoService, QuestaoService>();

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
