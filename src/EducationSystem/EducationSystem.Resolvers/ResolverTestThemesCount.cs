using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Resolvers
{
    public sealed class ResolverTestThemesCount : Resolver, IValueResolver<DatabaseTest, Test, int?>
    {
        private readonly IRepository<DatabaseTheme> _repositoryTheme;

        public ResolverTestThemesCount(IContext context, IRepository<DatabaseTheme> repositoryTheme) : base(context)
        {
            _repositoryTheme = repositoryTheme;
        }

        public int? Resolve(DatabaseTest source, Test destination, int? member, ResolutionContext context)
        {
            if (CurrentUser.IsAdmin())
                return _repositoryTheme.GetCount(new ThemesByTestId(source.Id));

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new ThemesByTestId(source.Id) &
                    new ThemesByLecturerId(CurrentUser.Id);

                return _repositoryTheme.GetCount(specification);
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new ThemesByTestId(source.Id) &
                    new ThemesForStudents() &
                    new ThemesByStudentId(CurrentUser.Id);

                return _repositoryTheme.GetCount(specification);
            }

            return null;
        }
    }
}