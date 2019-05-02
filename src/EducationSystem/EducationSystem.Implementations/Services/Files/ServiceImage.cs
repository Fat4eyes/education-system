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
    public sealed class ServiceImage : ServiceFile<Image>
    {
        public ServiceImage(
            IMapper mapper,
            ILogger<ServiceImage> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Image> validatorFile,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorFile,
                exceptionFactory,
                executionContext,
                repositoryFile)
        { }
    }
}