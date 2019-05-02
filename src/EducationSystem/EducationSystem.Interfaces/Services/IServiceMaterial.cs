using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceMaterial
    {
        Task<PagedData<Material>> GetMaterialsAsync(FilterMaterial filter);
        Task<PagedData<Material>> GetLecturerMaterialsAsync(int lecturerId, FilterMaterial filter);

        Task<Material> GetMaterialAsync(int id);
        Task<Material> GetStudentMaterialAsync(int id, int studentId);
        Task<Material> GetLecturerMaterialAsync(int id, int lecturerId);

        Task DeleteMaterialAsync(int id);
        Task UpdateMaterialAsync(int id, Material material);
        Task<int> CreateMaterialAsync(Material material);
    }
}