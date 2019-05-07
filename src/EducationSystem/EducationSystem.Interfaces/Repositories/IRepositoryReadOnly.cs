using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Models.Filters;
using EducationSystem.Specifications;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryReadOnly<TEntity> where TEntity : DatabaseModel
    {
        Task<List<TEntity>> FindAllAsync(ISpecification<TEntity> specification);

        Task<(int Count, List<TEntity> Items)> FindPaginatedAsync(ISpecification<TEntity> specification, Filter filter);

        Task<TEntity> FindFirstAsync(ISpecification<TEntity> specification);
    }
}