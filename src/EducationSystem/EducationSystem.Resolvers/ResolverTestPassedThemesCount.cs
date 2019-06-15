using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestPassedThemesCount : IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;

        public ResolverTestPassedThemesCount(IContext context, IRepository<DatabaseTheme> repositoryTheme)
        {
            _context = context;
            _repositoryTheme = repositoryTheme;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            var user = _context.GetCurrentUser();

            if (user.IsNotStudent())
                return null;

            var specification =
                new ThemesByTestId(source.Id) &
                new ThemesForStudents() &
                new ThemesByStudentId(user.Id, true);

            return _repositoryTheme.GetCount(specification);
        }
    }
}