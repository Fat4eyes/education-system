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
    public sealed class RepositoryTheme : Repository<DatabaseTheme>
    {
        public RepositoryTheme(DatabaseContext context) : base(context) { }

        public override Task<List<DatabaseTheme>> FindAllAsync(ISpecification<DatabaseTheme> specification)
        {
            return Entities
                .Where(specification.ToExpression())
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public override Task<(int Count, List<DatabaseTheme> Items)> FindPaginatedAsync
            (ISpecification<DatabaseTheme> specification, Filter filter)
        {
            return Entities
                .Where(specification.ToExpression())
                .OrderBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }
    }
}