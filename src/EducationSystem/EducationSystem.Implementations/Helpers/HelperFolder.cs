using System;
using EducationSystem.Constants;
using EducationSystem.Enums;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperFolder : IHelperFolder
    {
        public string GetFolderName(FileType type)
        {
            switch (type)
            {
                case FileType.Image:
                    return Directories.Images;
                case FileType.Document:
                    return Directories.Documents;
            }

            return Directories.Files;
        }

        public string GetFolderName(File file) =>
            GetFolderName(file?.Type ?? throw new ArgumentException(nameof(file)));
    }
}