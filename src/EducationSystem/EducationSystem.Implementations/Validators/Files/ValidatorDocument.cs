using EducationSystem.Implementations.Validators.Files.Basics;
using EducationSystem.Models.Files;

namespace EducationSystem.Implementations.Validators.Files
{
    public sealed class ValidatorDocument : ValidatorFile<Document>
    {
        /// <inheritdoc />
        protected override int MaxiFileSize => 25;

        /// <inheritdoc />
        protected override string[] AvailableExtensions { get; } =
        {
            ".txt",
            ".dat",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptm",
            ".pptx",
            ".zip",
            ".rar",
            ".dot",
            ".doc",
            ".docm",
            ".docx",
            ".pdf",
            ".rtf"
        };
    }
}