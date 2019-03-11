using System.Collections.Generic;
using EducationSystem.Helpers.Interfaces.Source.Files;

namespace EducationSystem.Helpers.Implementations.Source.Files
{
    public class HelperFileDocument : HelperFile, IHelperFileDocument
    {
        /// <inheritdoc />
        protected override int MaxiFileSize => 25;

        /// <inheritdoc />
        protected override List<string> AvailableExtensions { get; } = new List<string>
        {
            ".txt",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptm",
            ".pptx",
            ".zip",
            ".rar",
            ".doc",
            ".docm",
            ".docx",
            ".pdf"
        };
    }
}