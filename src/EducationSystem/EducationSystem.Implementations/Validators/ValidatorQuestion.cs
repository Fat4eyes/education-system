using System;
using System.Linq;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorQuestion : IValidator<Question>
    {
        private readonly IHelperFile _helperFile;

        private readonly IRepositoryFile _repositoryFile;
        private readonly IRepositoryTheme _repositoryTheme;
        private readonly IRepositoryMaterial _repositoryMaterial;

        public ValidatorQuestion(
            IHelperFile helperFile,
            IRepositoryFile repositoryFile,
            IRepositoryTheme repositoryTheme,
            IRepositoryMaterial repositoryMaterial)
        {
            _helperFile = helperFile;
            _repositoryFile = repositoryFile;
            _repositoryTheme = repositoryTheme;
            _repositoryMaterial = repositoryMaterial;
        }

        public void Validate(Question model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Text))
                throw ExceptionHelper.CreatePublicException("Не указан текст вопроса.");

            if (model.Time.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано время ответа на вопрос.");

            if (model.Time.Value <= 0 || model.Time.Value > 60 * 60)
                throw ExceptionHelper.CreatePublicException("Указано некорректное время ответа на вопрос.");

            if (model.Type.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указан тип вопроса.");

            if (model.Complexity.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана сложность вопроса.");

            if (model.ThemeId.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана тема.");

            if (_repositoryTheme.GetById(model.ThemeId.Value) == null)
                throw ExceptionHelper.CreatePublicException("Указанная тема не существует.");

            if (model.Image != null && _repositoryFile.GetById(model.Image.Id) == null)
                throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");

            if (model.Image != null && _helperFile.IsFileExists(model.Image) == false)
                throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");

            if (model.Material != null && _repositoryMaterial.GetById(model.Material.Id) == null)
                throw ExceptionHelper.CreatePublicException("Указанный материал не существует.");

            ValidateByQuestionType(model);
        }

        private static void ValidateByQuestionType(Question question)
        {
            if (question.Type == QuestionType.OpenedManyStrings)
                return;

            if (question.Type == QuestionType.WithProgram)
            {
                if (question.Program == null)
                    throw ExceptionHelper.CreatePublicException(
                        "Для вопроса не указана программа.");

                if (question.Program.TimeLimit.HasValue == false)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы не указано ограничение по времени.");

                if (question.Program.TimeLimit.Value < 1 || question.Program.TimeLimit.Value > 60)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы указано некорректное ограничение по времени.");

                if (question.Program.MemoryLimit.HasValue == false)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы не указано ограничение по памяти.");

                if (question.Program.MemoryLimit.Value < 1 || question.Program.MemoryLimit.Value > 10000)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы указано некорректное ограничение по памяти.");

                if (question.Program.LanguageType.HasValue == false)
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы не указан язык программирования.");

                if (question.Program.ProgramDatas.IsEmpty())
                    throw ExceptionHelper.CreatePublicException(
                        "Для программы не указаны входные и выходные параметры.");

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
                    throw ExceptionHelper.CreatePublicException(
                        "Для вопроса не указано ни одного варианта ответа.");

                if (question.Answers.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                    throw ExceptionHelper.CreatePublicException(
                        "Один или несколько вариантов ответа имеют неверный формат.");

                return;
            }

            if (question.Answers.IsEmpty() || question.Answers.Count < 2)
                throw ExceptionHelper.CreatePublicException(
                    "Для вопроса не указано нужное количество вариантов ответа. " +
                    "Минимальное количество вариантов ответа: 2.");

            if (question.Answers.All(x => x.IsRight != true))
                throw ExceptionHelper.CreatePublicException(
                    "Ни один вариант ответа не указан как правильный.");

            if (question.Answers.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                throw ExceptionHelper.CreatePublicException(
                    "Указанные варианты ответа имеют неверный формат.");

            if (question.Type != QuestionType.ClosedOneAnswer)
                return;

            if (question.Answers.Count(x => x.IsRight == true) > 1)
                throw ExceptionHelper.CreatePublicException("Указано более одного правильного ответа.");
        }
    }
}