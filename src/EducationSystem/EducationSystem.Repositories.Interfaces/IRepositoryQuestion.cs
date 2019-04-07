using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        (int Count, List<DatabaseQuestion> Questions) GetQuestions(FilterQuestion filter);
        (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, FilterQuestion filter);

        List<DatabaseQuestion> GetQuestionsForStudentByTestId(int testId, int studentId);
    }
}