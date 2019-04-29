using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestions(FilterQuestion filter);
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsByThemeId(int themeId, FilterQuestion filter);

        Task<List<DatabaseQuestion>> GetQuestionsForStudentByTestId(int testId, int studentId);

        Task<bool> IsQuestionExists(int id);

        Task<int> GetLastQuestionOrder(int themeId);
    }
}