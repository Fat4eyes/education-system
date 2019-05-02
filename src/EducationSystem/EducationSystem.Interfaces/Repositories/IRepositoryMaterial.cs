using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryMaterial : IRepository<DatabaseMaterial>
    {
        Task<(int Count, List<DatabaseMaterial> Materials)> GetMaterialsAsync(FilterMaterial filter);
        Task<(int Count, List<DatabaseMaterial> Materials)> GetUserMaterialsAsync(int userId, FilterMaterial filter);

        Task<DatabaseMaterial> GetUserMaterialAsync(int id, int userId);
    }
}