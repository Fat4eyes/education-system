using EducationSystem.Implementations.Validators.Files.Basics;
using EducationSystem.Models.Files;

namespace EducationSystem.Implementations.Validators.Files
{
    public sealed class ValidatorImage : ValidatorFile<Image>
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
    }
}