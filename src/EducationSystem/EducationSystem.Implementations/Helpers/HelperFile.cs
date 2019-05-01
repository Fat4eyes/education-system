using System;
using System.IO;
using System.Threading.Tasks;
using EducationSystem.Interfaces.Helpers;
using File = EducationSystem.Models.Files.Basics.File;
using SystemFile = System.IO.File;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperFile : IHelperFile
    {
        private readonly IHelperPath _helperPath;

        public HelperFile(IHelperPath helperPath)
        {
            _helperPath = helperPath;
        }

        public async Task<bool> FileExistsAsync(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(file.Path))
                return SystemFile.Exists(await _helperPath.GetAbsoluteFilePathAsync(file));

            var path = Path.Combine(_helperPath.GetContentPath(), file.Path.Replace("/", "\\"));

            return SystemFile.Exists(path);
        }
    }
}