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

        public (int Count, List<DatabaseTheme> Themes) GetThemes(OptionsTheme options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, OptionsTheme options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.ThemeTests.Any(y => y.TestId == testId))
                .ApplyPaging(options);

        public (int Count, List<DatabaseTheme> Themes) GetThemesByDisciplineId(int disciplineId, OptionsTheme options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.DisciplineId == disciplineId)
                .ApplyPaging(options);

        public DatabaseTheme GetThemeById(int id, OptionsTheme options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        protected override IQueryable<DatabaseTheme> IncludeByOptions(IQueryable<DatabaseTheme> query, OptionsTheme options)
        {
            if (options.WithQuestions)
                query = query.Include(x => x.Questions);

            return query;
        }
    }
}