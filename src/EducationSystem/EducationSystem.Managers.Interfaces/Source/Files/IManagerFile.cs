using System.Threading.Tasks;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Managers.Interfaces.Source.Files
{
    public interface IManagerFile
    {
        Task<File> AddFileAsync(File file);

        Task<File> GetFileById(int id);

        Task DeleteFileByIdAsync(int id);
    }
}