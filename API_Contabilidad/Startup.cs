using API_Contabilidad.Acceso_Datos;
using API_Contabilidad.Clases;
using API_Contabilidad.Clases.Seguridad;
using API_Contabilidad.Interfaces;
using API_Contabilidad.Logica_Negocio;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Contabilidad
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

            services.AddControllers();
            // Agregar CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            services.AddIdentityServer(options =>
            {
                // Aquí puedes configurar IdentityServer
                options.EmitStaticAudienceClaim = true;
              
            })
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryIdentityResources(Config.GetIdentityResources());

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:2817";
                    options.ApiName = "api1"; // El nombre de tu API que has configurado en Identity Server
                    options.RequireHttpsMetadata = false;
                });

            //swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Contabilidad", Version = "v1" });
            });


            //inyecciones de dependencias
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<MyDbContext>();
            services.AddScoped<VentasServices>();
            services.AddScoped<CalculosImportes>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Contabilidad v1"));
            }

            // Deshabilitar redireccionamiento a HTTPS
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAllOrigins");


            // Agrega el middleware de autenticación
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
        
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

