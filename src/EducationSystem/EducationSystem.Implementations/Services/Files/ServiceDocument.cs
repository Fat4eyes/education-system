using AutoMapper;
using EducationSystem.Implementations.Services.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using EducationSystem.Repositories.Interfaces;
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
            IRepositoryFile repositoryFile)
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