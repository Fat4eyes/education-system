using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestThemesCount : IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;

        public ResolverTestThemesCount(IContext context, IRepository<DatabaseTheme> repositoryTheme)
        {
            _context = context;
            _repositoryTheme = repositoryTheme;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            var user = _context.GetCurrentUser();

            if (user.IsAdmin())
                return _repositoryTheme.GetCount(new ThemesByTestId(source.Id));

            if (user.IsLecturer())
            {
                var specification =
                    new ThemesByTestId(source.Id) &
                    new ThemesByLecturerId(user.Id);

                return _repositoryTheme.GetCount(specification);
            }

            if (user.IsStudent())
            {
                var specification =
                    new ThemesByTestId(source.Id) &
                    new ThemesForStudents() &
                    new ThemesByStudentId(user.Id);

                return _repositoryTheme.GetCount(specification);
            }

            return null;
        }
    }
}