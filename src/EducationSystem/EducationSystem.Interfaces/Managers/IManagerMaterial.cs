using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerMaterial
    {
        Task<PagedData<Material>> GetMaterials(OptionsMaterial options, FilterMaterial filter);

        Task DeleteMaterial(int id);
        Task<Material> GetMaterial(int id, OptionsMaterial options);
        Task<Material> CreateMaterial(Material material);
        Task<Material> UpdateMaterial(int id, Material material);
    }
}