using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Services.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Files;
using EducationSystem.Models.Filters;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services.Files
{
    public sealed class ServiceImage : ServiceFile<Image>
    {
        public ServiceImage(
            IMapper mapper,
            IContext context,
            ILogger<ServiceImage> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Image> validatorFile,
            IRepository<DatabaseFile> repositoryFile)
            : base(
                mapper,
                context,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorFile,
                repositoryFile)
        { }

        public override async Task<PagedData<Image>> GetFilesAsync(FilterFile filter)
        {
            return await base.GetFilesAsync(filter.SetFileType(FileType.Image));
        }
    }
}