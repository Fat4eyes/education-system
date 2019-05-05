using AutoMapper;
using CodeExecutionSystem.Client.Extensions;
using EducationSystem.Dependencies;
using EducationSystem.WebApp.Source.Handlers;
using EducationSystem.WebApp.Source.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
                .AddJsonFile(ConfigurationHelper.GetConfigurationFileName(environment))
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddAutoMapper(
                ConfigurationHelper.ConfigureMapper,
                ConfigurationHelper.GetAssemblies());

            services.AddSpaStaticFiles(ConfigurationHelper.ConfigureSpaStaticFiles);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x => ConfigurationHelper.ConfigureJwtBearer(x, Configuration));

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(ConfigurationHelper.ConfigureJson);

            services.AddCodeExecutionSystem();

            DependencyRegistrar.Register(services, Configuration);
        }

        public void Configure(IApplicationBuilder builder, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Configuration.GetSection("LoggingParameters"));

            var _ = environment.IsDevelopment()
                ? builder.UseDeveloperExceptionPage()
                : builder.UseHsts();

            builder.UseStaticFiles(ConfigurationHelper.GetStaticFileOptions(environment));
            builder.UseSpaStaticFiles();
            builder.UseMiddleware(typeof(ExceptionHandler));
            builder.UseHttpsRedirection();

            builder.UseForwardedHeaders(ConfigurationHelper.GetForwardedHeadersOptions());
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