using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryMaterial : IRepository<DatabaseMaterial>
    {
        (int Count, List<DatabaseMaterial> Materials) GetMaterials(FilterMaterial filter);
    }
}