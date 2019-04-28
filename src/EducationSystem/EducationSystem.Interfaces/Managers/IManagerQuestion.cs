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
        Task<PagedData<Question>> GetQuestions(OptionsQuestion options, FilterQuestion filter);
        Task<PagedData<Question>> GetQuestionsByThemeId(int themeId, OptionsQuestion options, FilterQuestion filter);

        Task<List<Question>> GetQuestionsForStudentByTestId(int testId, int studentId);

        Task DeleteQuestion(int id);
        Task<Question> GetQuestion(int id, OptionsQuestion options);
        Task<Question> CreateQuestion(Question question);
        Task<Question> UpdateQuestion(int id, Question question);

        Task UpdateThemeQuestions(int themeId, List<Question> questions);
    }
}