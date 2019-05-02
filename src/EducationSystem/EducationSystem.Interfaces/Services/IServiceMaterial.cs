using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceMaterial
    {
        Task<PagedData<Material>> GetMaterialsAsync(OptionsMaterial options, FilterMaterial filter);
        Task<PagedData<Material>> GetLecturerMaterialsAsync(int lecturerId, OptionsMaterial options, FilterMaterial filter);

        Task<Material> GetMaterialAsync(int id, OptionsMaterial options);
        Task<Material> GetStudentMaterialAsync(int id, int studentId, OptionsMaterial options);
        Task<Material> GetLecturerMaterialAsync(int id, int lecturerId, OptionsMaterial options);

        Task DeleteMaterialAsync(int id);
        Task UpdateMaterialAsync(int id, Material material);
        Task<int> CreateMaterialAsync(Material material);
    }
}