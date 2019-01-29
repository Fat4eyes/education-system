using AutoMapper;
using EducationSystem.Dependencies.Source;
using EducationSystem.Mapping.Source;
using EducationSystem.WebApp.Source.Handlers;
using EducationSystem.WebApp.Source.Helpers;
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
        protected ConfigurationHelper ConfigurationHelper { get; }

        public Configurator(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("app.json")
                .Build();

            ConfigurationHelper = new ConfigurationHelper(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAutoMapper(MappingConfigurator.Configure);
            services.AddCors(ConfigurationHelper.ConfigureCors);

            services.AddSpaStaticFiles(ConfigurationHelper.ConfigureSpaStaticFiles);

            services
                .AddMvc()
                .AddJsonOptions(ConfigurationHelper.ConfigureJson);

            DependencyRecorder.Register(services, Configuration);
        }

        public void Configure(
            IApplicationBuilder builder,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            if (environment.IsDevelopment())
                builder.UseDeveloperExceptionPage();

            loggerFactory.AddFile(ConfigurationHelper.GetLoggingSection());

            builder.UseStaticFiles();
            builder.UseSpaStaticFiles();
           
            builder.UseMiddleware(typeof(ErrorHandler));

            builder.UseCors(x => x.WithOrigins(ConfigurationHelper.GetOrigins()));
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