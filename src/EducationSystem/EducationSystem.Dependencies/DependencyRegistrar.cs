using EducationSystem.Database;
using EducationSystem.Implementations;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Implementations.Managers;
using EducationSystem.Implementations.Managers.Files;
using EducationSystem.Implementations.Validators;
using EducationSystem.Implementations.Validators.Files;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Implementations;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.Dependencies
{
    public static class DependencyRegistrar
    {
        public static void Register(IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddTransient(x => configuration);

            RegisterDatabases(collection, configuration);

            collection.AddTransient<ITokenGenerator, TokenGenerator>();

            RegisterHelpers(collection);
            RegisterValidators(collection);
            RegisterManagers(collection);
            RegisterRepositories(collection);
        }

        private static void RegisterManagers(IServiceCollection collection)
        {
            collection.AddTransient<IManagerImage, ManagerImage>();
            collection.AddTransient<IManagerDocument, ManagerDocument>();

            collection.AddTransient<IManagerTest, ManagerTest>();
            collection.AddTransient<IManagerUser, ManagerUser>();
            collection.AddTransient<IManagerTheme, ManagerTheme>();
            collection.AddTransient<IManagerStudent, ManagerStudent>();
            collection.AddTransient<IManagerTestData, ManagerTestData>();
            collection.AddTransient<IManagerQuestion, ManagerQuestion>();
            collection.AddTransient<IManagerMaterial, ManagerMaterial>();
            collection.AddTransient<IManagerDiscipline, ManagerDiscipline>();
        }

        private static void RegisterHelpers(IServiceCollection collection)
        {
            collection.AddTransient<IHelperUserRole, HelperUserRole>();

            collection.AddTransient<IHelperFile, HelperFile>();
            collection.AddTransient<IHelperPath, HelperPath>();
            collection.AddTransient<IHelperFolder, HelperFolder>();
        }

        private static void RegisterValidators(IServiceCollection collection)
        {
            collection.AddTransient<IValidator<Test>, ValidatorTest>();
            collection.AddTransient<IValidator<Theme>, ValidatorTheme>();
            collection.AddTransient<IValidator<Question>, ValidatorQuestion>();
            collection.AddTransient<IValidator<Material>, ValidatorMaterial>();

            collection.AddTransient<IValidator<Image>, ValidatorImage>();
            collection.AddTransient<IValidator<Document>, ValidatorDocument>();
        }

        private static void RegisterRepositories(IServiceCollection collection)
        {
            collection.AddTransient<IRepositoryRole, RepositoryRole>();
            collection.AddTransient<IRepositoryTest, RepositoryTest>();
            collection.AddTransient<IRepositoryUser, RepositoryUser>();
            collection.AddTransient<IRepositoryFile, RepositoryFile>();
            collection.AddTransient<IRepositoryGroup, RepositoryGroup>();
            collection.AddTransient<IRepositoryTheme, RepositoryTheme>();
            collection.AddTransient<IRepositoryAnswer, RepositoryAnswer>();
            collection.AddTransient<IRepositoryProgram, RepositoryProgram>();
            collection.AddTransient<IRepositoryStudent, RepositoryStudent>();
            collection.AddTransient<IRepositoryQuestion, RepositoryQuestion>();
            collection.AddTransient<IRepositoryMaterial, RepositoryMaterial>();
            collection.AddTransient<IRepositoryTestTheme, RepositoryTestTheme>();
            collection.AddTransient<IRepositoryDiscipline, RepositoryDiscipline>();
            collection.AddTransient<IRepositoryProgramData, RepositoryProgramData>();
            collection.AddTransient<IRepositoryMaterialFile, RepositoryMaterialFile>();
        }

        private static void RegisterDatabases(IServiceCollection collection, IConfiguration configuration)
        {
            DatabaseRegistrar.Register(collection, configuration);
        }
    }
}