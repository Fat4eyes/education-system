using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestPassedThemesCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        public ResolverTestPassedThemesCount(IContext context) : base(context) { }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsNotStudent())
                return null;

            var specification =
                new ThemesForStudents() &
                new ThemesByStudentId(CurrentUser.Id, true);

            return source.TestThemes
                .Select(x => x.Theme)
                .Count(specification);
        }
    }
}