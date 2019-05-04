using System;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Factories
{
    public sealed class ExceptionFactory : IExceptionFactory
    {
        public Exception NotFound<TModel>(int id) where TModel : class
        {
            if (typeof(TModel) == typeof(DatabaseUser) || typeof(TModel) == typeof(User))
            {
                return ExceptionHelper.CreateNotFoundException(
                    $"Пользователь не найден. Идентификатор пользователя: {id}.", 
                    $"Пользователь не найден.");
            }

            if (typeof(TModel) == typeof(DatabaseFile) || typeof(TModel) == typeof(File))
            {
                return ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {id}.",
                    $"Файл не найден.");
            }

            if (typeof(TModel) == typeof(DatabaseDiscipline) || typeof(TModel) == typeof(Discipline))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Дисциплина не найдена. Идентификатор дисциплины: {id}.",
                    $"Дисциплина не найдена.");
            }

            if (typeof(TModel) == typeof(DatabaseMaterial) || typeof(TModel) == typeof(Material))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Материал не найден. Идентификатор материала: {id}.",
                    $"Материал не найден.");
            }

            if (typeof(TModel) == typeof(DatabaseQuestion) || typeof(TModel) == typeof(Question))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Вопрос не найден. Идентификатор вопроса: {id}.",
                    $"Вопрос не найден.");
            }

            if (typeof(TModel) == typeof(DatabaseTheme) || typeof(TModel) == typeof(Theme))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема не найдена. Идентификатор темы: {id}.",
                    $"Тема не найдена.");
            }

            if (typeof(TModel) == typeof(DatabaseTest) || typeof(TModel) == typeof(Test))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест не найден. Идентификатор теста: {id}.",
                    $"Тест не найден.");
            }

            if (typeof(TModel) == typeof(DatabaseProgram) || typeof(TModel) == typeof(Program))
            {
                throw ExceptionHelper.CreateNotFoundException(
                    $"Программа не найдена. Идентификатор программы: {id}.",
                    $"Программа не найдена.");
            }

            throw ExceptionHelper.CreateException($"Модель типа '{typeof(TModel)}' не поддерживается.");
        }

        public Exception NoAccess()
        {
            return ExceptionHelper.CreatePublicException("Не достаточно прав для выполнения данного действия.");
        }
    }
}