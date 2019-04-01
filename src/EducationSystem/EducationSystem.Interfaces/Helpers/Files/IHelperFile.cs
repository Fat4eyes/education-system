using EducationSystem.Enums;
using EducationSystem.Models;

namespace EducationSystem.Interfaces.Helpers.Files
{
    public interface IHelperFile
    {
        void ValidateFile(File file);

        bool FileExists(int id);

        bool FileExists(File file);

        string GetFilePath(File file);

        string GetFolderName(FileType type);
    }
}