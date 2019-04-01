﻿using EducationSystem.Database;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Implementations.Helpers.Files;
using EducationSystem.Implementations.Managers;
using EducationSystem.Implementations.Managers.Files;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Implementations.Validators;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Source.Rest;
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

            RegisterHelpers(collection);
            RegisterValidators(collection);
            RegisterManagers(collection);
            RegisterRepositories(collection);
        }

        private static void RegisterManagers(IServiceCollection collection)
        {
            collection.AddTransient<IManagerToken, ManagerToken>();

            collection.AddTransient<IManagerRole, ManagerRole>();
            collection.AddTransient<IManagerTest, ManagerTest>();
            collection.AddTransient<IManagerUser, ManagerUser>();
            collection.AddTransient<IManagerGroup, ManagerGroup>();
            collection.AddTransient<IManagerTheme, ManagerTheme>();
            collection.AddTransient<IManagerStudent, ManagerStudent>();
            collection.AddTransient<IManagerQuestion, ManagerQuestion>();
            collection.AddTransient<IManagerMaterial, ManagerMaterial>();
            collection.AddTransient<IManagerInstitute, ManagerInstitute>();
            collection.AddTransient<IManagerStudyPlan, ManagerStudyPlan>();
            collection.AddTransient<IManagerTestResult, ManagerTestResult>();
            collection.AddTransient<IManagerDiscipline, ManagerDiscipline>();
            collection.AddTransient<IManagerStudyProfile, ManagerStudyProfile>();

            collection.AddTransient<IManagerFileImage, ManagerFileImage>();
            collection.AddTransient<IManagerFileDocument, ManagerFileDocument>();
        }

        private static void RegisterHelpers(IServiceCollection collection)
        {
            collection.AddTransient<IHelperUser, HelperUser>();

            collection.AddTransient<IHelperFileImage, HelperFileImage>();
            collection.AddTransient<IHelperFileDocument, HelperFileDocument>();

            collection.AddTransient<IHelperQuestionTemplate, HelperQuestionTemplate>();
        }

        private static void RegisterValidators(IServiceCollection collection)
        {
            collection.AddTransient<IValidator<Test>, ValidatorTest>();
            collection.AddTransient<IValidator<Theme>, ValidatorTheme>();
            collection.AddTransient<IValidator<Question>, ValidatorQuestion>();
            collection.AddTransient<IValidator<Material>, ValidatorMaterial>();
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
            collection.AddTransient<IRepositoryInstitute, RepositoryInstitute>();
            collection.AddTransient<IRepositoryStudyPlan, RepositoryStudyPlan>();
            collection.AddTransient<IRepositoryTestTheme, RepositoryTestTheme>();
            collection.AddTransient<IRepositoryTestResult, RepositoryTestResult>();
            collection.AddTransient<IRepositoryDiscipline, RepositoryDiscipline>();
            collection.AddTransient<IRepositoryProgramData, RepositoryProgramData>();
            collection.AddTransient<IRepositoryStudyProfile, RepositoryStudyProfile>();
            collection.AddTransient<IRepositoryMaterialFile, RepositoryMaterialFile>();
        }

        private static void RegisterDatabases(IServiceCollection collection, IConfiguration configuration)
        {
            DatabaseRegistrar.Register(collection, configuration);
        }
    }
}