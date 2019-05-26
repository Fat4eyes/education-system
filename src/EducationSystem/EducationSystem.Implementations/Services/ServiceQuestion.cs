using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Disciplines;
using EducationSystem.Specifications.Questions;
using EducationSystem.Specifications.Tests;
using EducationSystem.Specifications.Themes;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceQuestion : Service<ServiceQuestion>, IServiceQuestion
    {
        private readonly IValidator<Question> _validatorQuestion;
        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;
        private readonly IRepository<DatabaseAnswer> _repositoryAnswer;
        private readonly IRepository<DatabaseProgram> _repositoryProgram;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;
        private readonly IRepository<DatabaseProgramData> _repositoryProgramData;
        private readonly IRepository<DatabaseQuestionMaterialAnchor> _repositoryQuestionMaterialAnchor;
        private readonly IQuestionValidatorFactory _questionValidatorFactory;

        public ServiceQuestion(
            IMapper mapper,
            IContext context,
            ILogger<ServiceQuestion> logger,
            IValidator<Question> validatorQuestion,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseTheme> repositoryTheme,
            IRepository<DatabaseAnswer> repositoryAnswer,
            IRepository<DatabaseProgram> repositoryProgram,
            IRepository<DatabaseQuestion> repositoryQuestion,
            IRepository<DatabaseProgramData> repositoryProgramData, 
            IRepository<DatabaseQuestionMaterialAnchor> repositoryQuestionMaterialAnchor,
            IQuestionValidatorFactory questionValidatorFactory)
            : base(mapper, context, logger)
        {
            _validatorQuestion = validatorQuestion;
            _repositoryTest = repositoryTest;
            _repositoryTheme = repositoryTheme;
            _repositoryAnswer = repositoryAnswer;
            _repositoryProgram = repositoryProgram;
            _repositoryQuestion = repositoryQuestion;
            _repositoryProgramData = repositoryProgramData;
            _repositoryQuestionMaterialAnchor = repositoryQuestionMaterialAnchor;
            _questionValidatorFactory = questionValidatorFactory;
        }

        public async Task<PagedData<Question>> GetQuestionsAsync(FilterQuestion filter)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var specification =
                    new QuestionsByTestId(filter.TestId) &
                    new QuestionsByThemeId(filter.ThemeId);

                var (count, questions) = await _repositoryQuestion.FindPaginatedAsync(specification, filter);

                return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
            }

            if (user.IsLecturer())
            {
                var specification =
                    new QuestionsByTestId(filter.TestId) &
                    new QuestionsByThemeId(filter.ThemeId) &
                    new QuestionsByLecturerId(user.Id);

                var (count, questions) = await _repositoryQuestion.FindPaginatedAsync(specification, filter);

                return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
            }

            if (user.IsStudent())
            {
                var specification =
                    new QuestionsForStudents() & 
                    new QuestionsByTestId(filter.TestId) &
                    new QuestionsByThemeId(filter.ThemeId) &
                    new QuestionsByStudentId(user.Id, filter.Passed);

                var (count, questions) = await _repositoryQuestion.FindPaginatedAsync(specification, filter);

                return new PagedData<Question>(Mapper.Map<List<Question>>(questions), count);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var question = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseQuestion>(id);

                return Mapper.Map<Question>(question);
            }

            if (user.IsLecturer())
            {
                var question = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseQuestion>(id);

                if (new QuestionsByLecturerId(user.Id).IsSatisfiedBy(question) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Question>(question);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotAdmin() && user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            var question = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseQuestion>(id);

            if (user.IsNotAdmin() && !new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(question.Theme?.Discipline))
                throw ExceptionHelper.NoAccess();

            await _repositoryQuestion.RemoveAsync(question, true);
        }

        public async Task<int> CreateQuestionAsync(Question question)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorQuestion.ValidateAsync(question.Format());

            var model = Mapper.Map<DatabaseQuestion>(question);

            model.Order = int.MaxValue;

            await ProcessMaterialAnchorsAsync(model, question);

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
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseQuestion>(id);

            if (!new ThemesByLecturerId(user.Id).IsSatisfiedBy(theme))
                throw ExceptionHelper.NoAccess();

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
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorQuestion.ValidateAsync(question.Format());

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseQuestion>(id);

            if (!new QuestionsByLecturerId(user.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseQuestion>(question), model);

            if (model.Answers.Any())
                await _repositoryAnswer.RemoveAsync(model.Answers, true);

            model.Answers = null;

            await _repositoryQuestion.UpdateAsync(model, true);

            await ProcessMaterialAnchorsAsync(model, question);

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

        public async Task<Question> ProcessTestQuestionAsync(int id, Question question)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotStudent())
                throw ExceptionHelper.NoAccess();

            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseTest>(id);

            var specification =
                new TestsById(id) &
                new TestsForStudents() &
                new TestsByStudentId(user.Id);

            if (specification.IsSatisfiedBy(test) == false)
                throw ExceptionHelper.CreatePublicException("Указанный тест недоступен.");

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(question.Id)) ??
                throw ExceptionHelper.NotFound<DatabaseQuestion>(question.Id);

            var result = await _questionValidatorFactory
                .GetQuestionValidator(model.Type)
                .ValidateAsync(question);

            if (result.Right != true)
                return result;

            model.QuestionStudents.Add(new DatabaseQuestionStudent
            {
                Passed = true,
                StudentId = user.Id
            });

            await _repositoryQuestion.UpdateAsync(model, true);

            return result;
        }

        private async Task ProcessMaterialAnchorsAsync(DatabaseQuestion model, Question question)
        {
            if (model.MaterialAnchors.Any())
                await _repositoryQuestionMaterialAnchor.RemoveAsync(model.MaterialAnchors, true);

            model.MaterialAnchors = question.Material != null && question.MaterialAnchors.IsNotEmpty()
                ? Mapper.Map<List<DatabaseQuestionMaterialAnchor>>(question.MaterialAnchors)
                : null;

            await _repositoryQuestionMaterialAnchor.UpdateAsync(model.MaterialAnchors, true);
        }
    }
}