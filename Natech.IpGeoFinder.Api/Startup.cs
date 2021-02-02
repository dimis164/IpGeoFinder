using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Natech.IpGeoFinder.Api.BackgroundServices;
using Natech.IpGeoFinder.DAL;
using Natech.IpGeoFinder.DAL.Interfaces;
using Natech.IpGeoFinder.DAL.Repositories;
using System.Reflection;

namespace Natech.IpGeoFinder.Api
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

            services.AddDbContext<GeoIpDBContext>();
            services.AddScoped<IGeoIpDbRepository, GeoIpDbRepository>();
            //services.AddSingleton<IGeoIpDbRepository, GeoIpDbRepository>();
            services.AddSingleton<BatchProcessingChannel>();
            services.AddHostedService<BatchService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IGeoIpRepository, GeoIpRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Natech.IpGeoFinder.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Natech.IpGeoFinder.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
