using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerQuestion
    {
        PagedData<Question> GetQuestions(OptionsQuestion options, FilterQuestion filter);
        PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options, FilterQuestion filter);

        List<Question> GetQuestionsForStudentByTestId(int testId, int studentId, FilterQuestion filter);

        Question GetQuestionById(int id, OptionsQuestion options);

        Task DeleteQuestionByIdAsync(int id);

        Task<Question> CreateQuestionAsync(Question question);

        Task<Question> UpdateQuestionAsync(int id, Question question);
    }
}