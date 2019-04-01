using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace EducationSystem.Implementations.Helpers.Files
{
    public sealed class HelperFileImage : HelperFile, IHelperFileImage
    {
        /// <inheritdoc />
        protected override int MaxiFileSize => 2;

        /// <inheritdoc />
        protected override string[] AvailableExtensions { get; } =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".bmp"
        };

        public HelperFileImage(
            IHostingEnvironment environment,
            IRepositoryFile repositoryFile)
            : base(
                environment,
                repositoryFile)
        { }
    }
}