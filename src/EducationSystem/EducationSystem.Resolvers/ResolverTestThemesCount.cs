using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestThemesCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        public ResolverTestThemesCount(IContext context) : base(context) { }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
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
                var specification =
                    new ThemesForStudents() &
                    new ThemesByStudentId(CurrentUser.Id);

                return source.TestThemes
                    .Select(x => x.Theme)
                    .Count(specification);
            }

            return null;
        }
    }
}