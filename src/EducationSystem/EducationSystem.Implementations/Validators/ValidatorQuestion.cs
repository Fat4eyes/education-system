using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorQuestion : IValidator<Question>
    {
        private readonly IHelperFile _helperFile;
        private readonly IExecutionContext _executionContext;
        private readonly IRepository<DatabaseFile> _repositoryFile;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;
        private readonly IRepository<DatabaseMaterial> _repositoryMaterial;

        public ValidatorQuestion(
            IHelperFile helperFile,
            IExecutionContext executionContext,
            IRepository<DatabaseFile> repositoryFile,
            IRepository<DatabaseTheme> repositoryTheme,
            IRepository<DatabaseMaterial> repositoryMaterial)
        {
            _helperFile = helperFile;
            _executionContext = executionContext;
            _repositoryFile = repositoryFile;
            _repositoryTheme = repositoryTheme;
            _repositoryMaterial = repositoryMaterial;
        }

        public async Task ValidateAsync(Question model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Text))
                throw ExceptionHelper.CreatePublicException("Не указан текст вопроса.");

            if (model.Time.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано время ответа на вопрос.");

            // Секунды.
            // От 1 до 3600 секунд.
            // От 1 секунды до 1 часа.
            if (model.Time.Value < 1 || model.Time.Value > 60 * 60)
                throw ExceptionHelper.CreatePublicException("Указано некорректное время ответа на вопрос.");

            if (model.Type.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указан тип вопроса.");

            if (model.Complexity.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана сложность вопроса.");

            if (model.ThemeId.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана тема.");

            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(model.ThemeId.Value)) ??
                throw ExceptionHelper.CreatePublicException("Указанная тема не существует.");

            var user = await _executionContext.GetCurrentUserAsync();

            if (new ThemesByLecturerId(user.Id).IsSatisfiedBy(theme) == false)
                throw ExceptionHelper.CreatePublicException("Указанная тема недоступна.");

            if (model.Image != null)
            {
                var image = await _repositoryFile.FindFirstAsync(new FilesById(model.Image.Id)) ??
                    throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");

                if (new FilesByOwnerId(user.Id).IsSatisfiedBy(image) == false)
                    throw ExceptionHelper.CreatePublicException("Указанное изображение недоступно.");

                if (await _helperFile.FileExistsAsync(model.Image) == false)
                    throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");
            }

            if (model.Material != null)
            {
                var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(model.Material.Id)) ??
                    throw ExceptionHelper.CreatePublicException("Указанный материал не существует.");

                if (new MaterialsByOwnerId(user.Id).IsSatisfiedBy(material) == false)
                    throw ExceptionHelper.CreatePublicException("Указанный материал недоступен.");
            }

            ValidateByQuestionType(model);
        }

        private static void ValidateByQuestionType(Question question)
        {
            if (question.Type == QuestionType.OpenedManyStrings)
                return;

            if (question.Type == QuestionType.WithProgram)
            {
                if (question.Program == null)
                    throw ExceptionHelper.CreatePublicException("Для вопроса не указана программа.");

                if (question.Program.TimeLimit.HasValue == false)
                    throw ExceptionHelper.CreatePublicException("Для программы не указано ограничение по времени.");

                // Секунды.
                // От 1 до 60 секунд.
                if (question.Program.TimeLimit.Value < 1 || question.Program.TimeLimit.Value > 60)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы указано некорректное ограничение по времени.");

                if (question.Program.MemoryLimit.HasValue == false)
                    throw ExceptionHelper.CreatePublicException("Для программы не указано ограничение по памяти.");

                // Килобайты.
                // От 5 до 10 мегабайтов.
                if (question.Program.MemoryLimit.Value < 5120 || question.Program.MemoryLimit.Value > 10240)
                    throw ExceptionHelper.CreatePublicException("Для программы указано некорректное ограничение по памяти.");

                if (question.Program.LanguageType.HasValue == false)
                    throw ExceptionHelper.CreatePublicException("Для программы не указан язык программирования.");

                if (question.Program.ProgramDatas.IsEmpty())
                    throw ExceptionHelper.CreatePublicException("Для программы не указаны входные и выходные параметры.");

                if (question.Program.ProgramDatas.Any(x =>
                    string.IsNullOrWhiteSpace(x.Input) ||
                    string.IsNullOrWhiteSpace(x.ExpectedOutput)))
                    throw ExceptionHelper.CreatePublicException(
                        "Некоторые входные или выходные параметры имеют неверный формат.");

                return;
            }

            if (question.Type == QuestionType.OpenedOneString)
            {
                if (question.Answers.IsEmpty())
                    throw ExceptionHelper.CreatePublicException("Для вопроса не указано ни одного варианта ответа.");

                if (question.Answers.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                    throw ExceptionHelper.CreatePublicException("Один или несколько вариантов ответа имеют неверный формат.");

                return;
            }

            if (question.Answers.IsEmpty() || question.Answers.Count < 2)
                throw ExceptionHelper.CreatePublicException(
                    "Для вопроса не указано нужное количество вариантов ответа. " +
                    "Минимальное количество вариантов ответа: 2.");

            if (question.Answers.All(x => x.IsRight != true))
                throw ExceptionHelper.CreatePublicException("Ни один вариант ответа не указан как правильный.");

            if (question.Answers.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                throw ExceptionHelper.CreatePublicException("Указанные варианты ответа имеют неверный формат.");

            if (question.Type != QuestionType.ClosedOneAnswer)
                return;

            if (question.Answers.Count(x => x.IsRight == true) > 1)
                throw ExceptionHelper.CreatePublicException("Указано более одного правильного ответа.");
        }
    }
}