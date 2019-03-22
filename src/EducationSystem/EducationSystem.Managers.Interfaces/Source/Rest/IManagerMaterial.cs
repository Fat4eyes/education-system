using System.Threading.Tasks;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerMaterial
    {
        PagedData<Material> GetMaterials(OptionsMaterial options, FilterMaterial filter);

        void DeleteMaterialById(int id);
        Task DeleteMaterialByIdAsync(int id);

        Material CreateMaterial(Material material);
        Task<Material> CreateMaterialAsync(Material material);

        Material UpdateMaterial(int id, Material material);
        Task<Material> UpdateMaterialAsync(int id, Material material);
    }
}