using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Services.Files
{
    public interface IServiceFile<TFile> where TFile : File
    {
        Task DeleteFileAsync(int id);
        Task<TFile> GetFileAsync(int id);
        Task<TFile> CreateFileAsync(TFile file);
    }
}