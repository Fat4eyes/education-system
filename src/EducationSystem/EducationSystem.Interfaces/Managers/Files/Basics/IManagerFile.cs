using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Managers.Files.Basics
{
    public interface IManagerFile<TFile> where TFile : File
    {
        Task<TFile> UploadFile(TFile file);

        Task<TFile> GetFileById(int id);

        Task DeleteFileByIdAsync(int id);

        string[] GetAvailableExtensions();
    }
}