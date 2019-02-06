using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTheme : RepositoryReadOnly<DatabaseTheme, OptionsTheme>, IRepositoryTheme
    {
        public RepositoryTheme(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, OptionsTheme options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options)
                .Where(x => x.ThemeTests.Any(y => y.TestId == testId))
                .ApplyPaging(options);
        }

        protected override IQueryable<DatabaseTheme> GetQueryableWithInclusions(OptionsTheme options)
        {
            var query = AsQueryable();

            if (options.WithDiscipline)
            {
                query = query.Include(x => x.Discipline);
            }

            return query;
        }
    }
}