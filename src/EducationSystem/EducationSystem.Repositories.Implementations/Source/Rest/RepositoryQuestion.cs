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

        public (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, FilterQuestion filter)
        {
            return AsQueryable()
                .Where(x => x.ThemeId == themeId)
                .ApplyPaging(filter);
        }

        public List<DatabaseQuestion> GetQuestionsForStudentByTestId(int testId, int studentId)
        {
            return AsQueryable()
                .Where(x => x.Theme.ThemeTests.Any(y =>
                    y.TestId == testId &&
                    y.Test.IsActive == 1 &&
                    y.Test.TestThemes.Any(z => z.Theme.Questions.Any())))
                .Where(x => x.Theme.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))))
                .ToList();
        }
    }
}