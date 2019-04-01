using System.Threading.Tasks;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerMaterial
    {
        PagedData<Material> GetMaterials(OptionsMaterial options, FilterMaterial filter);

        Task DeleteMaterialByIdAsync(int id);

        Task<Material> CreateMaterialAsync(Material material);

        Task<Material> UpdateMaterialAsync(int id, Material material);
    }
}