using System.Threading.Tasks;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Managers.Files.Basics
{
    public interface IManagerFile<TFile> where TFile : File
    {
        Task DeleteFile(int id);
        Task<TFile> GetFile(int id);
        Task<TFile> CreateFile(TFile file);
    }
}