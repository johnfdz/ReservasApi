using GDifare.Utilitario.BaseDatos;
using GDifare.Utilitario.Log;
using GDifare.Utilitario.Log.Elastic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Reservas.API.Datos;

namespace GDifare.Distribucion.Clientes.API
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Distribución Clientes HTTP API",
                    Description = "Distribución Clientes Microservicio HTTP API.",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Soporte",
                        Email = "soporte@grupodifare.com"
                    },
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            // Servicios MVC
            services.AddControllers();

            // HealthChecks
            services.AddHealthChecks();

            // Configuración del servicio
            services.AddTransient<ILogHandler, LogHandler>();
            services.AddTransient<ISqlServer, SqlServer>();
            services.AddTransient<IMapeoDatosReservas, MapeoDatosReservas>();
            services.AddTransient<IClienteElastic, ClienteElastic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "gdifare/api/docs/distribucion/clientes/{documentName}/clientes.json";
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/gdifare/api/docs/distribucion/clientes/v1/clientes.json", "Distribución Clientes API V1");
                    c.DocumentTitle = "Distribución Clientes";
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHealthChecks("/health/liveness");
                endpoints.MapHealthChecks("/health/readiness");
            });
        }
    }
}