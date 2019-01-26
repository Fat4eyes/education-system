using EducationSystem.Database.Source;
using EducationSystem.Managers.Implementations.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Implementations.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Dependencies.Source
{
    public static class DependencyRecorder
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(x => configuration);

            RegisterDatabase(services, configuration);

            RegisterManagers(services);
            RegisterRepositories(services);
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IManagerUser, ManagerUser>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepositoryUser, RepositoryUser>();
        }

        private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRecorder.Register(services, configuration);
        }
    }
}