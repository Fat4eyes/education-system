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
    /// <summary>
    /// Регистратор зависимостей приложения.
    /// </summary>
    public static class DependencyRecorder
    {
        /// <summary>
        /// Регистрирует зависимости приложения.
        /// </summary>
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(x => configuration);

            RegisterDatabase(services, configuration);

            RegisterManagers(services);
            RegisterRepositories(services);
        }

        /// <summary>
        /// Регистрирует менеджеры.
        /// </summary>
        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IConfigurationManager, ConfigurationManager>();

            services.AddTransient<IManagerUser, ManagerUser>();
            services.AddTransient<IManagerGroup, ManagerGroup>();
        }

        /// <summary>
        /// Регистрирует репозитории.
        /// </summary>
        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepositoryUser, RepositoryUser>();
            services.AddTransient<IRepositoryGroup, RepositoryGroup>();
        }

        /// <summary>
        /// Регистрирует базу данных.
        /// </summary>
        private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRecorder.Register(services, configuration);
        }
    }
}