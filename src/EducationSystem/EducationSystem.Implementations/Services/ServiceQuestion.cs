using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceQuestion : Service<ServiceQuestion>, IServiceQuestion
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Question> _validatorQuestion;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;

        private readonly IRepository<DatabaseTheme> _repositoryTheme;
        private readonly IRepository<DatabaseAnswer> _repositoryAnswer;
        private readonly IRepository<DatabaseProgram> _repositoryProgram;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;
        private readonly IRepository<DatabaseProgramData> _repositoryProgramData;

        public ServiceQuestion(
            IMapper mapper,
            ILogger<ServiceQuestion> logger,
            IHelperUserRole helperUserRole,
            IValidator<Question> validatorQuestion,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseTheme> repositoryTheme,
            IRepository<DatabaseAnswer> repositoryAnswer,
            IRepository<DatabaseProgram> repositoryProgram,
            IRepository<DatabaseQuestion> repositoryQuestion,
            IRepository<DatabaseProgramData> repositoryProgramData)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorQuestion = validatorQuestion;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryTheme = repositoryTheme;
            _repositoryAnswer = repositoryAnswer;
            _repositoryProgram = repositoryProgram;
            _repositoryQuestion = repositoryQuestion;
            _repositoryProgramData = repositoryProgramData;
        }

        public async Task<PagedData<Question>> GetQuestionsAsync(FilterQuestion filter)
        {
            var specification = new QuestionsByThemeId(filter.ThemeId);

            var (count, questions) = await _repositoryQuestion.FindPaginatedAsync(specification, filter);

            return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
        }

        public async Task<PagedData<Question>> GetLecturerQuestionsAsync(int lecturerId, FilterQuestion filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new QuestionsByThemeId(filter.ThemeId) &
                new QuestionsByLecturerId(lecturerId);

            var (count, questions) = await _repositoryQuestion.FindPaginatedAsync(specification, filter);

            return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            var question = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            return Mapper.Map<Question>(question);
        }

        public async Task<Question> GetLecturerQuestionAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new QuestionsById(id) &
                new QuestionsByLecturerId(lecturerId);

            var question = await _repositoryQuestion.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            return Mapper.Map<Question>(question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && !new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(question.Theme?.Discipline))
                throw _exceptionFactory.NoAccess();

            await _repositoryQuestion.RemoveAsync(question, true);
        }

        public async Task<int> CreateQuestionAsync(Question question)
        {
            await _validatorQuestion.ValidateAsync(question.Format());

            var model = Mapper.Map<DatabaseQuestion>(question);

            model.Order = int.MaxValue;

            switch (question.Type)
            {
                case QuestionType.OpenedOneString:
                case QuestionType.ClosedOneAnswer:
                case QuestionType.ClosedManyAnswers:
                    model.Answers = Mapper.Map<List<DatabaseAnswer>>(question.Answers);
                    break;
                case QuestionType.WithProgram:
                    model.Program = Mapper.Map<DatabaseProgram>(question.Program);
                    break;
            }

            await _repositoryQuestion.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateThemeQuestionsAsync(int id, List<Question> questions)
        {
            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && !new ThemesByLecturerId(user.Id).IsSatisfiedBy(theme))
                throw _exceptionFactory.NoAccess();

            var ids = questions.Select(x => x.Id).ToArray();

            var models = await _repositoryQuestion.FindAllAsync(new QuestionsByIds(ids));

            models.ForEach(x =>
            {
                var question = questions.FirstOrDefault(y => y.Id == x.Id);

                if (question?.Order != null && new QuestionsByThemeId(theme.Id).IsSatisfiedBy(x))
                    x.Order = question.Order.Value;
            });

            await _repositoryQuestion.UpdateAsync(models, true);
        }

        public async Task UpdateQuestionAsync(int id, Question question)
        {
            await _validatorQuestion.ValidateAsync(question.Format());

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (!new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(model.Theme?.Discipline))
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseQuestion>(question), model);

            if (model.Answers.Any())
                await _repositoryAnswer.RemoveAsync(model.Answers, true);

            model.Answers = null;

            await _repositoryQuestion.UpdateAsync(model, true);

            switch (question.Type)
            {
                case QuestionType.OpenedManyStrings:
                {
                    model.Program = null;

                    await _repositoryQuestion.UpdateAsync(model, true);

                    break;
                }
                case QuestionType.OpenedOneString:
                case QuestionType.ClosedOneAnswer:
                case QuestionType.ClosedManyAnswers:
                {
                    model.Program = null;

                    await _repositoryQuestion.UpdateAsync(model, true);

                    model.Answers = Mapper.Map<List<DatabaseAnswer>>(question.Answers);

                    await _repositoryAnswer.AddAsync(model.Answers, true);

                    break;
                }
                case QuestionType.WithProgram when model.Program == null:
                {
                    model.Program = Mapper.Map<DatabaseProgram>(question.Program);

                    await _repositoryProgram.AddAsync(model.Program, true);

                    break;
                }
                case QuestionType.WithProgram:
                {
                    Mapper.Map(Mapper.Map<DatabaseProgram>(question.Program), model.Program);

                    await _repositoryProgram.UpdateAsync(model.Program, true);

                    if (model.Program.ProgramDatas.Any())
                        await _repositoryProgramData.RemoveAsync(model.Program.ProgramDatas, true);

                    model.Program.ProgramDatas = Mapper.Map<List<DatabaseProgramData>>(question.Program.ProgramDatas);

                    await _repositoryProgramData.AddAsync(model.Program.ProgramDatas, true);

                    break;
                }
            }
        }
    }
}