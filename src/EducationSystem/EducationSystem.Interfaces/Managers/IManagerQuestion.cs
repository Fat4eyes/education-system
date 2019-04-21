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
        PagedData<Question> GetQuestions(OptionsQuestion options, FilterQuestion filter);
        PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options, FilterQuestion filter);

        List<Question> GetQuestionsForStudentByTestId(int testId, int studentId);

        Question GetQuestionById(int id, OptionsQuestion options);

        Task DeleteQuestionByIdAsync(int id);

        Task<Question> CreateQuestionAsync(Question question);

        Task<Question> UpdateQuestionAsync(int id, Question question);

        Task UpdateThemeQuestionsAsync(int themeId, List<Question> questions);
    }
}