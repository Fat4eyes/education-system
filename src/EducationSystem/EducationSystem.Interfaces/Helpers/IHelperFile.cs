using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperFile
    {
        bool FileExists(int id);

        bool FileExists(File file);

        string GetAbsoluteFilePath(File file);

        string GetRelativeFilePath(File file);
    }
}