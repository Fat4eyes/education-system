using System;
using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Factories
{
    public sealed class ExceptionFactory : IExceptionFactory
    {
        private static readonly Dictionary<Type, string> Messages = new Dictionary<Type, string>
        {
            [typeof(User)] = "Пользователь не найден.",
            [typeof(File)] = "Файл не найден.",
            [typeof(Test)] = "Тест не найден.",
            [typeof(Theme)] = "Тема не найдена.",
            [typeof(Program)] = "Программа не найдена.",
            [typeof(Material)] = "Материал не найден.",
            [typeof(Question)] = "Вопрос не найден.",
            [typeof(Discipline)] = "Дисциплина не найдена.",
            [typeof(DatabaseUser)] = "Пользователь не найден.",
            [typeof(DatabaseFile)] = "Файл не найден.",
            [typeof(DatabaseTest)] = "Тест не найден.",
            [typeof(DatabaseTheme)] = "Тема не найдена.",
            [typeof(DatabaseProgram)] = "Программа не найдена.",
            [typeof(DatabaseMaterial)] = "Материал не найден.",
            [typeof(DatabaseQuestion)] = "Вопрос не найден.",
            [typeof(DatabaseDiscipline)] = "Дисциплина не найдена."
        };

        public Exception NotFound<TModel>(int id) where TModel : class
        {
            if (Messages.TryGetValue(typeof(TModel), out var message))
                return ExceptionHelper.CreateNotFoundException($"{message} Идентификатор: {id}.", message);

            throw ExceptionHelper.CreateException($"Модель типа '{typeof(TModel)}' не поддерживается.");
        }

        public Exception NoAccess()
        {
            return ExceptionHelper.CreatePublicException("Не достаточно прав для выполнения данного действия или действие запрещено.");
        }
    }
}