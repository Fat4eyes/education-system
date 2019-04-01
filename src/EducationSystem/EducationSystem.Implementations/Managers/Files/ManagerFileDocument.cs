using AutoMapper;
using EducationSystem.Enums.Source;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Files
{
    public class ManagerFileDocument : ManagerFile, IManagerFileDocument
    {
        protected override FileType FileType => FileType.Document;

        public ManagerFileDocument(
            IMapper mapper,
            ILogger<ManagerFileDocument> logger,
            IHelperFileDocument helperFile,
            IHostingEnvironment environment,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperFile,
                environment,
                repositoryFile)
        { }
    }
}