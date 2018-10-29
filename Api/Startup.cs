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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Api.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

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
            services.AddTransient<IConsultaDeAlunosService, ConsultaDeAlunosService>();
            services.AddTransient<IConsultaEntregaDeLicaoService, ConsultaEntregaDeLicaoService>();
            services.AddTransient<IEntregaDeLicaoService, EntregaDeLicaoService>();
            services.AddTransient<IGeracaoDeEntregaDeLicaoService, GeracaoDeEntregaDeLicaoService>();
            services.AddTransient<IGrupoService, GrupoService>();
            services.AddTransient<ILicaoService, LicaoService>();
            services.AddTransient<ITrofeuService, TrofeuService>();
            services.AddTransient<IUsuarioService, UsuarioService>();

            AddJwtAuthorization(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UsuarioLogado>();
        }

        private void AddJwtAuthorization(IServiceCollection services)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                    {
                        var paramsValidation = bearerOptions.TokenValidationParameters;
                        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                        paramsValidation.ValidAudience = tokenConfigurations.Audience;
                        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                        paramsValidation.ValidateIssuerSigningKey = true;
                        paramsValidation.ValidateLifetime = true;
                        paramsValidation.ClockSkew = TimeSpan.Zero;
                    });

            services.AddAuthorization(auth =>
            {
                auth
                .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build()
                );
            });
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
