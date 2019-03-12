using EducationSystem.Database.Source;
using EducationSystem.Helpers.Implementations.Source;
using EducationSystem.Helpers.Implementations.Source.Files;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Implementations.Source;
using EducationSystem.Managers.Implementations.Source.Exports;
using EducationSystem.Managers.Implementations.Source.Files;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Export;
using EducationSystem.Managers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Implementations.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Dependencies.Source
{
    public static class DependencyRegistrar
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(x => configuration);

            RegisterDatabases(services, configuration);

            RegisterHelpers(services);
            RegisterManagers(services);
            RegisterRepositories(services);
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IManagerToken, ManagerToken>();

            services.AddTransient<IManagerRole, ManagerRole>();
            services.AddTransient<IManagerTest, ManagerTest>();
            services.AddTransient<IManagerUser, ManagerUser>();
            services.AddTransient<IManagerGroup, ManagerGroup>();
            services.AddTransient<IManagerTheme, ManagerTheme>();
            services.AddTransient<IManagerStudent, ManagerStudent>();
            services.AddTransient<IManagerQuestion, ManagerQuestion>();
            services.AddTransient<IManagerInstitute, ManagerInstitute>();
            services.AddTransient<IManagerStudyPlan, ManagerStudyPlan>();
            services.AddTransient<IManagerTestResult, ManagerTestResult>();
            services.AddTransient<IManagerDiscipline, ManagerDiscipline>();
            services.AddTransient<IManagerStudyProfile, ManagerStudyProfile>();

            services.AddTransient<IManagerFileImage, ManagerFileImage>();
            services.AddTransient<IManagerFileDocument, ManagerFileDocument>();

            services.AddTransient<IManagerExportQuestion, ManagerExportQuestion>();
        }

        private static void RegisterHelpers(IServiceCollection services)
        {
            services.AddTransient<IHelperUser, HelperUser>();
            services.AddTransient<IHelperTest, HelperTest>();
            services.AddTransient<IHelperTheme, HelperTheme>();
            services.AddTransient<IHelperQuestion, HelperQuestion>();

            services.AddTransient<IHelperFileImage, HelperFileImage>();
            services.AddTransient<IHelperFileDocument, HelperFileDocument>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepositoryRole, RepositoryRole>();
            services.AddTransient<IRepositoryTest, RepositoryTest>();
            services.AddTransient<IRepositoryUser, RepositoryUser>();
            services.AddTransient<IRepositoryGroup, RepositoryGroup>();
            services.AddTransient<IRepositoryTheme, RepositoryTheme>();
            services.AddTransient<IRepositoryAnswer, RepositoryAnswer>();
            services.AddTransient<IRepositoryProgram, RepositoryProgram>();
            services.AddTransient<IRepositoryStudent, RepositoryStudent>();
            services.AddTransient<IRepositoryQuestion, RepositoryQuestion>();
            services.AddTransient<IRepositoryInstitute, RepositoryInstitute>();
            services.AddTransient<IRepositoryStudyPlan, RepositoryStudyPlan>();
            services.AddTransient<IRepositoryTestTheme, RepositoryTestTheme>();
            services.AddTransient<IRepositoryTestResult, RepositoryTestResult>();
            services.AddTransient<IRepositoryDiscipline, RepositoryDiscipline>();
            services.AddTransient<IRepositoryProgramData, RepositoryProgramData>();
            services.AddTransient<IRepositoryStudyProfile, RepositoryStudyProfile>();
        }

        private static void RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRegistrar.Register(services, configuration);
        }
    }
}