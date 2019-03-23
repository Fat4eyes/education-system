using System.Threading.Tasks;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Managers.Interfaces.Source.Files
{
    public interface IManagerFile
    {
        File SaveFile(File file);
        Task<File> SaveFileAsync(File file);

        bool FileExists(File file);
    }
}