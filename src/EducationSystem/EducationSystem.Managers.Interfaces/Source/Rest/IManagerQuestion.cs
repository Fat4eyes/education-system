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

        List<Question> GetQuestionsForStudentByTestId(int testId, int studentId, int questionsCount);

        Question GetQuestionById(int id, OptionsQuestion options);

        void DeleteQuestionById(int id);
        Task DeleteQuestionByIdAsync(int id);

        Question CreateQuestion(Question question);
        Task<Question> CreateQuestionAsync(Question question);

        Question UpdateQuestion(int id, Question question);
        Task<Question> UpdateQuestionAsync(int id, Question question);
    }
}