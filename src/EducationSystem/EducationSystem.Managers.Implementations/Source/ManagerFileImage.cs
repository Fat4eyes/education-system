using AutoMapper;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source
{
    public class ManagerFileImage : ManagerFile, IManagerFileImage
    {
        protected override string FolderName => "Images";

        public ManagerFileImage(
            IMapper mapper,
            ILogger<ManagerFileImage> logger,
            IHelperFileImage helperFile,
            IHostingEnvironment environmen)
            : base(mapper, logger, helperFile, environmen)
        { }
    }
}