using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Dependencies.Source;
using EducationSystem.Managers.Implementations.Source;
using EducationSystem.Mapping.Source;
using EducationSystem.WebApp.Source.Helpers;
using EducationSystem.WebApp.Source.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.WebApp.Source.Rest
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

        public void Configure(IApplicationBuilder builder, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                builder.UseDeveloperExceptionPage();

            builder.UseMiddleware(typeof(ErrorHandler));

            var items = ConfigurationManager
                .GetCorsSection()
                .Get<List<Cors>>()
                .Select(x => x.Origin)
                .ToArray();

            builder.UseCors(x => x.WithOrigins(items));
            builder.UseMvc();
        }
    }
}