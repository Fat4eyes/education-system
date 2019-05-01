using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryQuestion : IRepository<DatabaseQuestion>
    {
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsAsync(FilterQuestion filter);
        Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsByThemeIdAsync(int themeId, FilterQuestion filter);

        Task<List<DatabaseQuestion>> GetQuestionsForStudentByTestIdAsync(int testId, int studentId);

        Task<int> GetLastQuestionOrderAsync(int themeId);
    }
}