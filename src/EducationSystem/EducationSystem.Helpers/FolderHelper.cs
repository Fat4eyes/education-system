using EducationSystem.Constants;
using EducationSystem.Enums;

namespace EducationSystem.Helpers
{
    public static class FolderHelper
    {
        public static string GetFolderName(FileType type)
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
    }
}