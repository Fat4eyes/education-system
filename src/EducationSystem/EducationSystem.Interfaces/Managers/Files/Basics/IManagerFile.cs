using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Managers.Files.Basics
{
    public interface IManagerFile<in TFile> where TFile : File
    {
        Task<File> UploadFile(TFile file);

        Task<File> GetFileById(int id);

        Task DeleteFileByIdAsync(int id);
    }
}