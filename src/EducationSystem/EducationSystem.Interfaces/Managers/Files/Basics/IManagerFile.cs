using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Managers.Files.Basics
{
    public interface IManagerFile<TFile> where TFile : File
    {
        Task DeleteFileAsync(int id);
        Task<TFile> GetFileAsync(int id);
        Task<TFile> CreateFileAsync(TFile file);
    }
}