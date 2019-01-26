using EducationSystem.Dependencies.Source;
using EducationSystem.Managers.Implementations.Source;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EducationSystem.WebApp.Source.Helpers;

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
            services.AddMvc();
            services.AddOptions();

            services.AddCors(ConfigurationHelper.ConfigureCors);

            DependencyRecorder.Register(services, Configuration);
        }

        public void Configure(IApplicationBuilder builder, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                builder.UseDeveloperExceptionPage();

            builder.UseCors(ConfigurationManager.GetCorsPolicy());
            builder.UseMvc();
        }
    }
}