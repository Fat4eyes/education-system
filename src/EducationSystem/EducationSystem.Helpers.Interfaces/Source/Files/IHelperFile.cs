using EducationSystem.Enums.Source;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Helpers.Interfaces.Source.Files
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