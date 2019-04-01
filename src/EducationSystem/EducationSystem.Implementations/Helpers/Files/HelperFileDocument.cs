using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace EducationSystem.Implementations.Helpers.Files
{
    public sealed class HelperFileDocument : HelperFile, IHelperFileDocument
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

        public HelperFileDocument(
            IHostingEnvironment environment,
            IRepositoryFile repositoryFile)
            : base(
                environment,
                repositoryFile)
        { }
    }
}