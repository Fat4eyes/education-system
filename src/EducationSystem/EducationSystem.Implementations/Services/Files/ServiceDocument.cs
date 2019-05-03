using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Services.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services.Files
{
    public sealed class ServiceDocument : ServiceFile<Document>
    {
        public ServiceDocument(
            IMapper mapper,
            ILogger<ServiceDocument> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Document> validatorDocument,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseFile> repositoryFile)
            : base(
                mapper,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorDocument,
                exceptionFactory,
                executionContext,
                repositoryFile)
        { }
    }
}