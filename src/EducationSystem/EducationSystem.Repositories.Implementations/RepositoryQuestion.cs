using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryQuestion : Repository<DatabaseQuestion>, IRepositoryQuestion
    {
        public RepositoryQuestion(DatabaseContext context)
            : base(context) { }

        public Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestions(FilterQuestion filter)
        {
            return AsQueryable()
                .OrderBy(x => x.Order)
                .ApplyPaging(filter);
        }

        public Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsByThemeId(int themeId, FilterQuestion filter)
        {
            return AsQueryable()
                .Where(x => x.ThemeId == themeId)
                .OrderBy(x => x.Order)
                .ApplyPaging(filter);
        }

        public async Task<List<DatabaseQuestion>> GetQuestionsForStudentByTestId(int testId, int studentId)
        {
            var questionTypes = new List<QuestionType>
            {
                QuestionType.ClosedOneAnswer,
                QuestionType.ClosedManyAnswers,
                QuestionType.WithProgram,
                QuestionType.OpenedOneString
            };

            return await AsQueryable()
                .Where(x => x.Theme.ThemeTests.Any(y =>
                    y.TestId == testId &&
                    y.Test.IsActive == 1 &&
                    y.Test.TestThemes.Any(z => z.Theme.Questions.Any())))
                .Where(x => questionTypes.Contains(x.Type))
                .Where(x => x.Theme.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))))
                .Where(x => x.QuestionStudents.Any(y => y.StudentId == studentId && y.Passed) == false)
                .OrderBy(x => x.Theme.Order)
                .ThenBy(x => x.Order)
                .Take(TestParamaters.QuestionsCountForStudent)
                .ToListAsync();
        }

        public Task<bool> IsQuestionExists(int id)
        {
            return AsQueryable().AnyAsync(x => x.Id == id);
        }

        public Task<int> GetLastQuestionOrder(int themeId)
        {
            return AsQueryable()
                .Where(x => x.ThemeId == themeId && x.Order.HasValue)
                .MaxAsync(x => x.Order.Value);
        }
    }
}