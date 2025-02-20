using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sponte.App;
using Sponte.App.Contratos;
using Sponte.Dt;
using Sponte.Dt.Context;
using Sponte.Dt.Contratos;
using System;


namespace Sponte.Api
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
            
            services.AddControllers().AddNewtonsoftJson(j => j.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnSql")));
            services.AddScoped<IGeralDt, GeralDt>();
            services.AddScoped<IIntrutorService, InstrutorService>();
            services.AddScoped<IInstrutorDt, InstrutorDt>();
            services.AddScoped<ILiveService, LiveService>();
            services.AddScoped<ILiveDt, LiveDt>();
            services.AddScoped<IInscritoService, InscritoService>();
            services.AddScoped<IInscritoDt, InscritoDt>();
            services.AddScoped<IInscricaoService, InscricaoService>();
            services.AddScoped<IInscricaoDt, InscricaoDt>();






            services.AddControllers();
            services.AddCors();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sponte.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sponte.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
