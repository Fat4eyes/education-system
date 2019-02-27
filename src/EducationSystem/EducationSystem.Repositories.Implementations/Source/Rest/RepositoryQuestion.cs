using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryQuestion : RepositoryReadOnly<DatabaseQuestion>, IRepositoryQuestion
    {
        public RepositoryQuestion(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, Filter filter)
        {
            return AsQueryable()
                .Where(x => x.ThemeId == themeId)
                .ApplyPaging(filter);
        }
    }
}