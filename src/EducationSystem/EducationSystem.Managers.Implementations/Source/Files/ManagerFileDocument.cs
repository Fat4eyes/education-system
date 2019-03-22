using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Enums.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Files;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Files
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