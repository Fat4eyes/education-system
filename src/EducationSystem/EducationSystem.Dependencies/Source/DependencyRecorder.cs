using EducationSystem.Managers.Implementations.Source;
using EducationSystem.Managers.Implementations.Source.Examples;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Dependencies.Source
{
    public static class DependencyRecorder
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(x => configuration);

            Register(services);
        }

        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IExampleManager, ExampleManager>();
        }
    }
}