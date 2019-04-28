using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperFile
    {
        bool IsFileExists(File file);

        string GetAbsoluteFilePath(File file);

        string GetRelativeFilePath(File file);
    }
}