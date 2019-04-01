using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryMaterial : IRepository<DatabaseMaterial>
    {
        (int Count, List<DatabaseMaterial> Materials) GetMaterials(FilterMaterial filter);
    }
}