using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Datas;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestDataThemes : Resolver, IValueResolver<DatabaseTest, TestData, int?>
    {
        public ResolverTestDataThemes(IExecutionContext executionContext)
            : base(executionContext) { }

        public int? Resolve(DatabaseTest source, TestData destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsAdmin())
            {
                return source.TestThemes
                    .Select(x => x.Theme)
                    .Count();
            }

            if (CurrentUser.IsLecturer())
            {
                return source.TestThemes
                    .Select(x => x.Theme)
                    .Count(new ThemesByLecturerId(CurrentUser.Id));
            }

            if (CurrentUser.IsStudent())
            {
                return source.TestThemes
                    .Select(x => x.Theme)
                    .Count(new ThemesForStudents());
            }

            return null;
        }
    }
}