using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Files
{
    public class ManagerFileImage : ManagerFile, IManagerFileImage
    {
        protected override string FolderName => Directories.Images;

        public ManagerFileImage(
            IMapper mapper,
            ILogger<ManagerFileImage> logger,
            IHelperFileImage helperFile,
            IHostingEnvironment environmen)
            : base(mapper, logger, helperFile, environmen)
        { }
    }
}