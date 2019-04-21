using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerQuestion : Manager<ManagerQuestion>, IManagerQuestion
    {
        private readonly IHelperPath _helperPath;
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Question> _validatorQuestion;

        private readonly IRepositoryTheme _repositoryTheme;
        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryProgram _repositoryProgram;
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryProgramData _repositoryProgramData;

        public ManagerQuestion(
            IMapper mapper,
            ILogger<ManagerQuestion> logger,
            IHelperPath helperPath,
            IHelperUserRole helperUserRole,
            IValidator<Question> validatorQuestion,
            IRepositoryTheme repositoryTheme,
            IRepositoryAnswer repositoryAnswer,
            IRepositoryProgram repositoryProgram,
            IRepositoryQuestion repositoryQuestion,
            IRepositoryProgramData repositoryProgramData)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorQuestion = validatorQuestion;
            _repositoryTheme = repositoryTheme;
            _repositoryAnswer = repositoryAnswer;
            _repositoryProgram = repositoryProgram;
            _repositoryQuestion = repositoryQuestion;
            _repositoryProgramData = repositoryProgramData;
            _helperPath = helperPath;
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

        public List<Question> GetQuestionsForStudentByTestId(int testId, int studentId)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            return _repositoryQuestion
                .GetQuestionsForStudentByTestId(testId, studentId)
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
            _validatorQuestion.Validate(question);

            FormatQuestion(question);

            var model = Mapper.Map<DatabaseQuestion>(question);

            if (question.ThemeId.HasValue)
                model.Order = _repositoryQuestion.GetLastQuestionOrder(question.ThemeId.Value) + 1;

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
            _validatorQuestion.Validate(question);

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

        public async Task UpdateThemeQuestionsAsync(int themeId, List<Question> questions)
        {
            if (questions.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Не указаны вопросы для обновления.");

            if (questions.GroupBy(x => x.Id).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("Указаны повторяющиеся вопросы.");

            if (_repositoryQuestion.IsQuestionsExists(questions.Select(x => x.Id).ToList()) == false)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных вопросов не существуют.");

            var theme = _repositoryTheme.GetById(themeId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема не найдена. Идентификатор темы: {themeId}.",
                    $"Тема не найдена.");

            if (theme.Questions.Count != questions.Count)
                throw ExceptionHelper.CreatePublicException("Количество указанных вопросов не совпадает с количеством вопросов в теме.");

            if (theme.Questions.All(x => questions.Select(y => y.Id).Contains(x.Id)) == false)
                throw ExceptionHelper.CreatePublicException("У одного или нескольких вопросов указанная тема не совпадает.");

            var models = _repositoryQuestion.GetByIds(questions.Select(x => x.Id).ToArray());

            var order = 1;

            questions.ForEach(x =>
            {
                var model = models.FirstOrDefault(y => y.Id == x.Id) ??
                    throw ExceptionHelper.CreateNotFoundException(
                        $"Вопрос не найден. Идентификатор вопроса: {x.Id}.",
                        $"Вопрос не найден.");

                model.Order = order++;
            });

            await _repositoryQuestion.UpdateAsync(models, true);
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

                    if (d.Image != null)
                        d.Image.Path = GetFilePath(s.Image);
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
                    
                    if (d.Image != null)
                        d.Image.Path = GetFilePath(s.Image);

                    d.Answers.ForEach(y => y.IsRight = null);
                });
            });
        }

        private string GetFilePath(DatabaseFile file)
        {
            if (file == null) return null;

            return _helperPath
                .GetRelativeFilePath(file)
                .Replace("\\", "/");
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