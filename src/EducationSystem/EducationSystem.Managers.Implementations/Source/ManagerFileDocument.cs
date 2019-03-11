using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source
{
    public class ManagerFileDocument : ManagerFile, IManagerFileDocument
    {
        protected override string FolderName => Directories.Documents;

        public ManagerFileDocument(
            IMapper mapper,
            ILogger<ManagerFileDocument> logger,
            IHelperFileDocument helperFile,
            IHostingEnvironment environment)
            : base(mapper, logger, helperFile, environment)
        { }
    }
}