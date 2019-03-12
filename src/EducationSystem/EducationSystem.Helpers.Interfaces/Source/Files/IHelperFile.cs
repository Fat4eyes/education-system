using EducationSystem.Models.Source.Files;

namespace EducationSystem.Helpers.Interfaces.Source.Files
{
    public interface IHelperFile
    {
        void ValidateFile(File file);

        bool FileExists(File file);
    }
}