using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryMaterial : IRepository<DatabaseMaterial>
    {
        Task<(int Count, List<DatabaseMaterial> Materials)> GetMaterialsAsync(FilterMaterial filter);
    }
}