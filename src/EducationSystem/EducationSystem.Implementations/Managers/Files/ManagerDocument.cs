using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Implementations.Managers.Files.Basics;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Files
{
    public sealed class ManagerDocument : ManagerFile<Document>, IManagerDocument
    {
        public ManagerDocument(
            IMapper mapper,
            ILogger<ManagerDocument> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Document> validatorDocument,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorDocument,
                repositoryFile)
        { }

        public override string[] GetAvailableExtensions() => FileExtensions.AvailableDocumentExtensions;
    }
}