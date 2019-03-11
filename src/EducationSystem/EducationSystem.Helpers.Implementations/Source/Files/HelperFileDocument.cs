using System.Collections.Generic;
using EducationSystem.Helpers.Interfaces.Source.Files;
using Microsoft.AspNetCore.Hosting;

namespace EducationSystem.Helpers.Implementations.Source.Files
{
    public class HelperFileDocument : HelperFile, IHelperFileDocument
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

        public HelperFileDocument(IHostingEnvironment environment)
            : base(environment) { }
    }
}