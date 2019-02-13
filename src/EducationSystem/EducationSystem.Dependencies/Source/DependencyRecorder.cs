using EducationSystem.Database.Source;
using EducationSystem.Helpers.Implementations.Source;
using EducationSystem.Helpers.Interfaces.Source;
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
            RegisterHelpers(services);
            RegisterRepositories(services);
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IManagerToken, ManagerToken>();

            services.AddTransient<IManagerUser, ManagerUser>();
            services.AddTransient<IManagerGroup, ManagerGroup>();
            services.AddTransient<IManagerStudyPlan, ManagerStudyPlan>();
            services.AddTransient<IManagerStudyProfile, ManagerStudyProfile>();
            services.AddTransient<IManagerInstitute, ManagerInstitute>();
            services.AddTransient<IManagerRole, ManagerRole>();
            services.AddTransient<IManagerTest, ManagerTest>();
            services.AddTransient<IManagerTestResult, ManagerTestResult>();
            services.AddTransient<IManagerTheme, ManagerTheme>();
            services.AddTransient<IManagerQuestion, ManagerQuestion>();
            services.AddTransient<IManagerDiscipline, ManagerDiscipline>();
            services.AddTransient<IManagerStudent, ManagerStudent>();
        }

        private static void RegisterHelpers(IServiceCollection services)
        {
            services.AddTransient<IUserHelper, UserHelper>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepositoryUser, RepositoryUser>();
            services.AddTransient<IRepositoryGroup, RepositoryGroup>();
            services.AddTransient<IRepositoryStudyPlan, RepositoryStudyPlan>();
            services.AddTransient<IRepositoryStudyProfile, RepositoryStudyProfile>();
            services.AddTransient<IRepositoryInstitute, RepositoryInstitute>();
            services.AddTransient<IRepositoryRole, RepositoryRole>();
            services.AddTransient<IRepositoryTest, RepositoryTest>();
            services.AddTransient<IRepositoryTestResult, RepositoryTestResult>();
            services.AddTransient<IRepositoryTheme, RepositoryTheme>();
            services.AddTransient<IRepositoryQuestion, RepositoryQuestion>();
            services.AddTransient<IRepositoryDiscipline, RepositoryDiscipline>();
            services.AddTransient<IRepositoryStudent, RepositoryStudent>();
        }

        private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRecorder.Register(services, configuration);
        }
    }
}