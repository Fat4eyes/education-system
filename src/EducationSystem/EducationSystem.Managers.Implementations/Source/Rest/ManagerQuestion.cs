using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerQuestion : Manager<ManagerQuestion>, IManagerQuestion
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryQuestion _repositoryQuestion;

        public ManagerQuestion(
            IMapper mapper,
            ILogger<ManagerQuestion> logger,
            IUserHelper userHelper,
            IRepositoryQuestion repositoryQuestion)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryQuestion = repositoryQuestion;
        }

        public PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options, FilterQuestion filter)
        {
            var (count, questions) = _repositoryQuestion.GetQuestionsByThemeId(themeId, filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        public List<Question> GetQuestionsForStudentByTestId(int testId, int studentId, int questionsCount)
        {
            if (!_userHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var questions = _repositoryQuestion.GetQuestionsForStudentByTestId(testId, studentId);

            questions = questions
                .Mix()
                .Take(questionsCount)
                .ToList();

            return questions.Select(MapForStudent).ToList();
        }

        private Question Map(DatabaseQuestion question, OptionsQuestion options)
        {
            return Mapper.Map<DatabaseQuestion, Question>(question, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithAnswers)
                        d.Answers = Mapper.Map<List<Answer>>(s.Answers);

                    if (options.WithProgram)
                        d.Program = Mapper.Map<Program>(s.Program);
                });
            });
        }

        private Question MapForStudent(DatabaseQuestion question)
        {
            return Mapper.Map<DatabaseQuestion, Question>(question, x =>
            {
                x.AfterMap((s, d) =>
                {
                    d.Program = Mapper.Map<Program>(s.Program);
                    d.Answers = Mapper.Map<List<Answer>>(s.Answers);

                    // Для студента не нужно показывать, какой ответ правильный.
                    d.Answers.ForEach(y => y.IsRight = null);
                });
            });
        }
    }
}