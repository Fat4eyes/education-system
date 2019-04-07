﻿using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerMaterial
    {
        PagedData<Material> GetMaterials(OptionsMaterial options, FilterMaterial filter);

        Task DeleteMaterialByIdAsync(int id);

        Material GetMaterialById(int id, OptionsMaterial options);

        Task<Material> CreateMaterialAsync(Material material);

        Task<Material> UpdateMaterialAsync(int id, Material material);
    }
}