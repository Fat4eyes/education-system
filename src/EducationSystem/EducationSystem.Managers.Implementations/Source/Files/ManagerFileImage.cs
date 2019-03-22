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
    public class ManagerFileImage : ManagerFile, IManagerFileImage
    {
        protected override FileType FileType => FileType.Document;

        public ManagerFileImage(
            IMapper mapper,
            ILogger<ManagerFileImage> logger,
            IHelperFileImage helperFile,
            IHostingEnvironment environmen,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperFile,
                environmen,
                repositoryFile)
        { }
    }
}