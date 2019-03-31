using System;
using System.Linq;
using EducationSystem.Enums.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Helpers.Implementations.Source
{
    public sealed class HelperQuestion : IHelperQuestion
    {
        private readonly IHelperFileImage _helperFileImage;

        private readonly IRepositoryFile _repositoryFile;
        private readonly IRepositoryTheme _repositoryTheme;
        private readonly IRepositoryMaterial _repositoryMaterial;

        public HelperQuestion(
            IHelperFileImage helperFileImage,
            IRepositoryFile repositoryFile,
            IRepositoryTheme repositoryTheme,
            IRepositoryMaterial repositoryMaterial)
        {
            _helperFileImage = helperFileImage;
            _repositoryFile = repositoryFile;
            _repositoryTheme = repositoryTheme;
            _repositoryMaterial = repositoryMaterial;
        }

        public void ValidateQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            if (string.IsNullOrWhiteSpace(question.Text))
                throw ExceptionHelper.CreatePublicException("Не указан текст вопроса.");

            if (question.Time.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано время ответа на вопрос.");

            if (question.Time.Value <= 0 || question.Time.Value > 60 * 60)
                throw ExceptionHelper.CreatePublicException("Указано некорректное время ответа на вопрос.");

            if (question.Type.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указан тип вопроса.");

            if (question.Complexity.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана сложность вопроса.");

            if (question.ThemeId.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указана тема.");

            if (_repositoryTheme.GetById(question.ThemeId.Value) == null)
                throw ExceptionHelper.CreatePublicException("Указанная тема не существует.");

            if (question.ImageId.HasValue && _repositoryFile.GetById(question.ImageId.Value) == null)
                throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");

            if (question.ImageId.HasValue && _helperFileImage.FileExists(question.ImageId.Value) == false)
                throw ExceptionHelper.CreatePublicException("Указанное изображение не существует.");

            if (question.MaterialId.HasValue && _repositoryMaterial.GetById(question.MaterialId.Value) == null)
                throw ExceptionHelper.CreatePublicException("Указанный материал не существует.");

            ValidateByQuestionType(question);
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