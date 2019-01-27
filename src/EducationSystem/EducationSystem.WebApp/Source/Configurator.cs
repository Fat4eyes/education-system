using AutoMapper;
using EducationSystem.Dependencies.Source;
using EducationSystem.Managers.Implementations.Source;
using EducationSystem.Mapping.Source;
using EducationSystem.WebApp.Source.Handlers;
using EducationSystem.WebApp.Source.Helpers;
using EducationSystem.WebApp.Source.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationSystem.WebApp.Source
{
    public class Configurator
    {
        protected IConfiguration Configuration { get; }
        protected ConfigurationManager ConfigurationManager { get; }
        protected ConfigurationHelper ConfigurationHelper { get; }

        public Configurator(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("app.json")
                .Build();

            ConfigurationManager = new ConfigurationManager(Configuration);
            ConfigurationHelper = new ConfigurationHelper(ConfigurationManager);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAutoMapper(MappingConfigurator.Configure);
            services.AddCors(ConfigurationHelper.ConfigureCors);

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

            loggerFactory.AddFile(ConfigurationManager.GetLoggingSection());

            builder.UseMiddleware(typeof(ErrorHandler));

            var cors = ConfigurationManager
                .GetCorsSection()
                .Get<Cors>();

            builder.UseCors(x => x.WithOrigins(cors.Origins.ToArray()));
            builder.UseMvc();
        }
    }
}