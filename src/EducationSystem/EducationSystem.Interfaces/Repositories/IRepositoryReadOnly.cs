using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Interfaces.Specifications;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        Task<List<TModel>> FindAllAsync(ISpecification<TModel> specification);

        Task<(int Count, List<TModel> Items)> FindPaginatedAsync(ISpecification<TModel> specification, Filter filter);

        Task<TModel> FindFirstAsync(ISpecification<TModel> specification);
    }
}