using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestPassedThemesCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IRepository<DatabaseTheme> _repositoryTheme;

        public ResolverTestPassedThemesCount(IContext context, IRepository<DatabaseTheme> repositoryTheme) : base(context)
        {
            _repositoryTheme = repositoryTheme;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsNotStudent())
                return null;

            var specification =
                new ThemesByTestId(source.Id) &
                new ThemesForStudents() &
                new ThemesByStudentId(CurrentUser.Id, true);

            return _repositoryTheme.GetCount(specification);
        }
    }
}