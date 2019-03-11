using System.Collections.Generic;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Helpers.Implementations.Source.Files
{
    public class HelperFileImage : HelperFile, IHelperFileImage
    {
        /// <inheritdoc />
        protected override int MaxiFileSize => 2;

        /// <inheritdoc />
        protected override List<string> AvailableExtensions { get; } = new List<string>
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif"
        };

        public override void ValidateFile(File file)
        {
            base.ValidateFile(file);
        }
    }
}