using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        (int Count, List<DatabaseQuestion> Questions) GetQuestions(FilterQuestion filter);
        (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, FilterQuestion filter);

        List<DatabaseQuestion> GetQuestionsForStudentByTestId(int testId, int studentId);
    }
}