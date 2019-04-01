using EducationSystem.Database.Source;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Implementations.Helpers.Files;
using EducationSystem.Implementations.Managers;
using EducationSystem.Implementations.Managers.Files;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Repositories.Implementations;
using EducationSystem.Repositories.Interfaces;
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
            services.AddTransient<IManagerMaterial, ManagerMaterial>();
            services.AddTransient<IManagerInstitute, ManagerInstitute>();
            services.AddTransient<IManagerStudyPlan, ManagerStudyPlan>();
            services.AddTransient<IManagerTestResult, ManagerTestResult>();
            services.AddTransient<IManagerDiscipline, ManagerDiscipline>();
            services.AddTransient<IManagerStudyProfile, ManagerStudyProfile>();

            services.AddTransient<IManagerFileImage, ManagerFileImage>();
            services.AddTransient<IManagerFileDocument, ManagerFileDocument>();
        }

        private static void RegisterHelpers(IServiceCollection services)
        {
            services.AddTransient<IHelperUser, HelperUser>();
            services.AddTransient<IHelperTest, HelperTest>();
            services.AddTransient<IHelperTheme, HelperTheme>();
            services.AddTransient<IHelperQuestion, HelperQuestion>();
            services.AddTransient<IHelperMaterial, HelperMaterial>();

            services.AddTransient<IHelperFileImage, HelperFileImage>();
            services.AddTransient<IHelperFileDocument, HelperFileDocument>();

            services.AddTransient<IHelperQuestionTemplate, HelperQuestionTemplate>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepositoryRole, RepositoryRole>();
            services.AddTransient<IRepositoryTest, RepositoryTest>();
            services.AddTransient<IRepositoryUser, RepositoryUser>();
            services.AddTransient<IRepositoryFile, RepositoryFile>();
            services.AddTransient<IRepositoryGroup, RepositoryGroup>();
            services.AddTransient<IRepositoryTheme, RepositoryTheme>();
            services.AddTransient<IRepositoryAnswer, RepositoryAnswer>();
            services.AddTransient<IRepositoryProgram, RepositoryProgram>();
            services.AddTransient<IRepositoryStudent, RepositoryStudent>();
            services.AddTransient<IRepositoryQuestion, RepositoryQuestion>();
            services.AddTransient<IRepositoryMaterial, RepositoryMaterial>();
            services.AddTransient<IRepositoryInstitute, RepositoryInstitute>();
            services.AddTransient<IRepositoryStudyPlan, RepositoryStudyPlan>();
            services.AddTransient<IRepositoryTestTheme, RepositoryTestTheme>();
            services.AddTransient<IRepositoryTestResult, RepositoryTestResult>();
            services.AddTransient<IRepositoryDiscipline, RepositoryDiscipline>();
            services.AddTransient<IRepositoryProgramData, RepositoryProgramData>();
            services.AddTransient<IRepositoryStudyProfile, RepositoryStudyProfile>();
            services.AddTransient<IRepositoryMaterialFile, RepositoryMaterialFile>();
        }

        private static void RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRegistrar.Register(services, configuration);
        }
    }
}