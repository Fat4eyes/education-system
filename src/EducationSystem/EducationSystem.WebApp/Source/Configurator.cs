using AutoMapper;
using EducationSystem.Dependencies.Source;
using EducationSystem.WebApp.Source.Handlers;
using EducationSystem.WebApp.Source.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationSystem.WebApp.Source
{
    public class Configurator
    {
        protected IConfiguration Configuration { get; }

        public Configurator(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("app.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAutoMapper(ConfigurationHelper.ConfigureMapper);
            services.AddSpaStaticFiles(ConfigurationHelper.ConfigureSpaStaticFiles);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(ConfigurationHelper.ConfigureJwtBearer);

            services.AddMvc();

            DependencyRecorder.Register(services, Configuration);
        }

        public void Configure(IApplicationBuilder builder, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            if (environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }
            else
            {
                builder.UseHsts();
            }

            builder.UseStaticFiles();
            builder.UseSpaStaticFiles();
            builder.UseMiddleware(typeof(ExceptionHandler));
            builder.UseHttpsRedirection();
            builder.UseAuthentication();
            builder.UseMvc();

            builder.UseSpa(spa =>
            {
                spa.Options.SourcePath = "App";

                if (environment.IsDevelopment())
                    spa.UseReactDevelopmentServer("start");
            });
        }
    }
}