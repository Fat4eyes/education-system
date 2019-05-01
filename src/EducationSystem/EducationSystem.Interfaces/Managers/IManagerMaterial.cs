using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerMaterial
    {
        Task<PagedData<Material>> GetMaterialsAsync(OptionsMaterial options, FilterMaterial filter);

        Task DeleteMaterialAsync(int id);
        Task<Material> GetMaterialAsync(int id, OptionsMaterial options);
        Task<Material> CreateMaterialAsync(Material material);
        Task<Material> UpdateMaterialAsync(int id, Material material);
    }
}