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
    public class RepositoryQuestion : RepositoryReadOnly<DatabaseQuestion, OptionsQuestion>, IRepositoryQuestion
    {
        public RepositoryQuestion(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, OptionsQuestion options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        protected override IQueryable<DatabaseQuestion> IncludeByOptions(IQueryable<DatabaseQuestion> query, OptionsQuestion options)
        {
            if (options.WithAnswers)
                query = query.Include(x => x.Answers);

            return query;
        }
    }
}