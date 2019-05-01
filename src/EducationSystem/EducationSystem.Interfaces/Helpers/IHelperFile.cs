using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperFile
    {
        Task<bool> FileExistsAsync(File file);
    }
}