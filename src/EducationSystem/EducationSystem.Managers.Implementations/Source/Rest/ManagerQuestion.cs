using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;
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
        private readonly IHelperUser _helperUser;
        private readonly IHelperQuestion _helperQuestion;

        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryProgram _repositoryProgram;
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryProgramData _repositoryProgramData;

        public ManagerQuestion(
            IMapper mapper,
            ILogger<ManagerQuestion> logger,
            IHelperUser helperUser,
            IHelperQuestion helperQuestion,
            IRepositoryAnswer repositoryAnswer,
            IRepositoryProgram repositoryProgram,
            IRepositoryQuestion repositoryQuestion,
            IRepositoryProgramData repositoryProgramData)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _helperQuestion = helperQuestion;
            _repositoryAnswer = repositoryAnswer;
            _repositoryProgram = repositoryProgram;
            _repositoryQuestion = repositoryQuestion;
            _repositoryProgramData = repositoryProgramData;
        }

        public PagedData<Question> GetQuestions(OptionsQuestion options, FilterQuestion filter)
        {
            var (count, questions) = _repositoryQuestion.GetQuestions(filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options, FilterQuestion filter)
        {
            var (count, questions) = _repositoryQuestion.GetQuestionsByThemeId(themeId, filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        public List<Question> GetQuestionsForStudentByTestId(int testId, int studentId, FilterQuestion filter)
        {
            _helperUser.CheckRoleStudent(studentId);

            var questions = _repositoryQuestion.GetQuestionsForStudentByTestId(testId, studentId, filter);

            questions = questions
                .Mix()
                .Take(filter.Count)
                .ToList();

            return questions
                .Select(MapForStudent)
                .ToList();
        }

        public Question GetQuestionById(int id, OptionsQuestion options)
        {
            var question = _repositoryQuestion.GetById(id) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Вопрос не найден. Идентификатор вопроса: {id}.",
                   $"Вопрос не найден.");

            return Map(question, options);
        }

        public async Task DeleteQuestionByIdAsync(int id)
        {
            var question = _repositoryQuestion.GetById(id) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Вопрос для удаления не найден. Идентификатор вопроса: {id}.",
                   $"Вопрос для удаления не найден.");

            await _repositoryQuestion.RemoveAsync(question, true);
        }

        public async Task<Question> CreateQuestionAsync(Question question)
        {
            _helperQuestion.ValidateQuestion(question);

            FormatQuestion(question);

            var model = Mapper.Map<DatabaseQuestion>(question);

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

            return Mapper.Map<DatabaseQuestion, Question>(model);
        }

        public async Task<Question> UpdateQuestionAsync(int id, Question question)
        {
            _helperQuestion.ValidateQuestion(question);

            var model = _repositoryQuestion.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Вопрос для обновления не найден. Идентификатор вопроса: {id}.",
                    $"Вопрос для обновления не найден.");

            FormatQuestion(question);

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

            return Mapper.Map<DatabaseQuestion, Question>(model);
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

                    if (options.WithMaterial)
                        d.Material = Mapper.Map<Material>(s.Material);
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
                    d.Material = Mapper.Map<Material>(s.Material);

                    d.Answers.ForEach(y => y.IsRight = null);
                });
            });
        }

        private static void FormatQuestion(Question question)
        {
            question.Text = question.Text.Trim();

            question.Answers?.ForEach(answer =>
            {
                answer.Text = answer.Text.Trim();
            });

            if (question.Program != null)
            {
                question.Program.Template = question.Program.Template?.Trim();

                if (string.IsNullOrWhiteSpace(question.Program.Template))
                    question.Program.Template = null;
            }
        }
    }
}