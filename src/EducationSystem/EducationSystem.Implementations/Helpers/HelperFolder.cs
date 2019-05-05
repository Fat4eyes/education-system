using System;
using EducationSystem.Enums;
using EducationSystem.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperFolder : IHelperFolder
    {
        public static string GetFolderName(FileType type)
        {
            return FolderHelper.GetFolderName(type);
        }

        string IHelperFolder.GetFolderName(FileType type)
        {
            return GetFolderName(type);
        }

        public string GetFolderName(File file) =>
            GetFolderName(file?.Type ?? throw new ArgumentNullException(nameof(file)));
    }
}