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

        Task<Material> GetMaterialAsync(int id, OptionsMaterial options);

        Task DeleteMaterialAsync(int id);
        Task UpdateMaterialAsync(int id, Material material);
        Task<int> CreateMaterialAsync(Material material);
    }
}