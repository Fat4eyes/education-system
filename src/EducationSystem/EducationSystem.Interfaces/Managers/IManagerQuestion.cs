using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerQuestion
    {
        Task<PagedData<Question>> GetQuestionsAsync(OptionsQuestion options, FilterQuestion filter);
        Task<PagedData<Question>> GetQuestionsByThemeIdAsync(int themeId, OptionsQuestion options, FilterQuestion filter);

        Task<List<Question>> GetQuestionsForStudentByTestIdAsync(int testId, int studentId);

        Task DeleteQuestionAsync(int id);
        Task<Question> GetQuestionAsync(int id, OptionsQuestion options);
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(int id, Question question);

        Task UpdateThemeQuestionsAsync(int themeId, List<Question> questions);
    }
}