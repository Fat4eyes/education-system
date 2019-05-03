using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerQuestion : Manager, IManagerQuestion
    {
        private readonly IServiceQuestion _serviceQuestion;

        public ManagerQuestion(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceQuestion serviceQuestion)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceQuestion = serviceQuestion;
        }

        public async Task<PagedData<Question>> GetQuestionsAsync(FilterQuestion filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceQuestion.GetQuestionsAsync(filter);

            if (CurrentUser.IsLecturer())
                return await _serviceQuestion.GetLecturerQuestionsAsync(CurrentUser.Id, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceQuestion.GetQuestionAsync(id);

            if (CurrentUser.IsLecturer())
                return await _serviceQuestion.GetLecturerQuestionAsync(id, CurrentUser.Id);

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceQuestion.DeleteQuestionAsync(id);
        }

        public async Task UpdateQuestionAsync(int id, Question question)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceQuestion.UpdateQuestionAsync(id, question);
        }

        public async Task<int> CreateQuestionAsync(Question question)
        {
            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer())
                return await _serviceQuestion.CreateQuestionAsync(question);

            throw ExceptionFactory.NoAccess();
        }

        public async Task UpdateThemeQuestionsAsync(int id, List<Question> questions)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceQuestion.UpdateThemeQuestionsAsync(id, questions);
        }
    }
}