using System;
using System.IO;
using EducationSystem.Constants;
using EducationSystem.Enums;

namespace EducationSystem.Helpers
{
    public static class PathHelper
    {
        public static string GetRelativeFilePath(FileType type, Guid guid, string name)
        {
            return Path.Combine(Directories.Files, FolderHelper.GetFolderName(type), guid + Path.GetExtension(name));
        }
    }
}