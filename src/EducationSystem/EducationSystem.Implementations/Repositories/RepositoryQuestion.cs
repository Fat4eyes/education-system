using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryQuestion : Repository<DatabaseQuestion>
    {
        public RepositoryQuestion(DatabaseContext context) : base(context) { }

        public override Task<List<DatabaseQuestion>> FindAllAsync(ISpecification<DatabaseQuestion> specification)
        {
            return Entities
                .Where(specification.ToExpression())
                .OrderBy(x => x.Theme.Order)
                .ThenBy(x => x.Order)
                .ToListAsync();
        }

        public override Task<(int Count, List<DatabaseQuestion> Items)> FindPaginatedAsync
            (ISpecification<DatabaseQuestion> specification, Filter filter)
        {
            return Entities
                .Where(specification.ToExpression())
                .OrderBy(x => x.Theme.Order)
                .ThenBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }

        public override Task<DatabaseQuestion> FindFirstAsync(ISpecification<DatabaseQuestion> specification)
        {
            return Entities
                .OrderBy(x => x.Theme.Order)
                .ThenBy(x => x.Order)
                .FirstOrDefaultAsync(specification.ToExpression());
        }
    }
}