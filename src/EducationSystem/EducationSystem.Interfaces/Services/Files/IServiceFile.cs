using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Services.Files
{
    public interface IServiceFile<TFile> where TFile : File
    {
        Task<PagedData<TFile>> GetFilesAsync(FilterFile filter);

        Task DeleteFileAsync(int id);
        Task<TFile> GetFileAsync(int id);
        Task<TFile> CreateFileAsync(TFile file);
    }
}