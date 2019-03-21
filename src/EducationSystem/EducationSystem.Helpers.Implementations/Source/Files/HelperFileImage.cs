using EducationSystem.Helpers.Interfaces.Source.Files;
using Microsoft.AspNetCore.Hosting;

namespace EducationSystem.Helpers.Implementations.Source.Files
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

        public HelperFileImage(IHostingEnvironment environment)
            : base(environment) { }
    }
}