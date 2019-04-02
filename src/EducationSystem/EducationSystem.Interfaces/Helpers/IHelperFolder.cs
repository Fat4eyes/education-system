using EducationSystem.Enums;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperFolder
    {
        string GetFolderName(FileType type);

        string GetFolderName(File file);
    }
}