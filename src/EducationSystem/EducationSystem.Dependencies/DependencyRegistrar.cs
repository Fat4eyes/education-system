using CodeExecutionSystem.Contracts.Abstractions;
using EducationSystem.Database;
using EducationSystem.Database.Models;
using EducationSystem.Implementations;
using EducationSystem.Implementations.Factories;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Implementations.Repositories;
using EducationSystem.Implementations.Services;
using EducationSystem.Implementations.Services.Files;
using EducationSystem.Implementations.Validators;
using EducationSystem.Implementations.Validators.Files;
using EducationSystem.Implementations.Validators.Questions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using EducationSystem.Models.Rest;
using Microsoft.AspNetCore.Http;
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

            collection.AddScoped<IContext, Context>();

            collection.AddTransient<ICodeExecutor, CodeExecutor>();
            collection.AddTransient<IHashComputer, HashComputer>();
            collection.AddTransient<ITokenGenerator, TokenGenerator>();
            collection.AddTransient<IQuestionValidatorFactory, QuestionValidatorFactory>();

            // TODO: Потом удалить. Представляет собой реализацию фейковых ответов.
            collection.AddTransient<ICodeExecutionApi, CodeExecutionApi>();

            collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            RegisterHelpers(collection);
            RegisterValidators(collection);
            RegisterServices(collection);
            RegisterRepositories(collection);
        }

        private static void RegisterServices(IServiceCollection collection)
        {
            collection.AddTransient<IServiceFile<Image>, ServiceImage>();
            collection.AddTransient<IServiceFile<Document>, ServiceDocument>();

            collection.AddTransient<IServiceUser, ServiceUser>();
            collection.AddTransient<IServiceTest, ServiceTest>();
            collection.AddTransient<IServiceTheme, ServiceTheme>();
            collection.AddTransient<IServiceQuestion, ServiceQuestion>();
            collection.AddTransient<IServiceMaterial, ServiceMaterial>();
            collection.AddTransient<IServiceDiscipline, ServiceDiscipline>();
        }

        private static void RegisterHelpers(IServiceCollection collection)
        {
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

            collection.AddTransient<IQuestionValidatorClosedOneAnswer, QuestionValidatorClosedOneAnswer>();
            collection.AddTransient<IQuestionValidatorClosedManyAnswers, QuestionValidatorClosedManyAnswers>();
            collection.AddTransient<IQuestionValidatorOpenedOneString, QuestionValidatorOpenedOneString>();
            collection.AddTransient<IQuestionValidatorWithProgram, QuestionValidatorWithProgram>();
        }

        private static void RegisterRepositories(IServiceCollection collection)
        {
            collection.AddTransient<IRepository<DatabaseTheme>, RepositoryTheme>();
            collection.AddTransient<IRepository<DatabaseQuestion>, RepositoryQuestion>();

            collection.AddTransient<IRepository<DatabaseRole>, Repository<DatabaseRole>>();
            collection.AddTransient<IRepository<DatabaseUser>, Repository<DatabaseUser>>();
            collection.AddTransient<IRepository<DatabaseTest>, Repository<DatabaseTest>>();
            collection.AddTransient<IRepository<DatabaseFile>, Repository<DatabaseFile>>();
            collection.AddTransient<IRepository<DatabaseGroup>, Repository<DatabaseGroup>>();
            collection.AddTransient<IRepository<DatabaseAnswer>, Repository<DatabaseAnswer>>();
            collection.AddTransient<IRepository<DatabaseProgram>, Repository<DatabaseProgram>>();
            collection.AddTransient<IRepository<DatabaseMaterial>, Repository<DatabaseMaterial>>();
            collection.AddTransient<IRepository<DatabaseTestTheme>, Repository<DatabaseTestTheme>>();
            collection.AddTransient<IRepository<DatabaseDiscipline>, Repository<DatabaseDiscipline>>();
            collection.AddTransient<IRepository<DatabaseProgramData>, Repository<DatabaseProgramData>>();
            collection.AddTransient<IRepository<DatabaseMaterialFile>, Repository<DatabaseMaterialFile>>();
            collection.AddTransient<IRepository<DatabaseQuestionStudent>, Repository<DatabaseQuestionStudent>>();
        }

        private static void RegisterDatabases(IServiceCollection collection, IConfiguration configuration)
        {
            DatabaseRegistrar.Register(collection, configuration);
        }
    }
}