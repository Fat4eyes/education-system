using System;
using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Rest;

namespace EducationSystem.Helpers
{
    public static class ExceptionHelper
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

        public static Exception NotFound<TModel>(int id) where TModel : class
        {
            if (Messages.TryGetValue(typeof(TModel), out var message))
                return CreateNotFoundException($"{message} Идентификатор: {id}.", message);

            throw CreateException($"Модель типа '{typeof(TModel)}' не поддерживается.");
        }

        public static Exception NoAccess()
        {
            return CreatePublicException("Не достаточно прав для выполнения данного действия.");
        }

        public static EducationSystemException CreateException(string @private)
            => new EducationSystemException(@private);

        public static EducationSystemException CreateException(string @private, string @public)
            => new EducationSystemException(@private, CreatePublicException(@public));

        public static EducationSystemNotFoundException CreateNotFoundException(string @private, string @public)
            => new EducationSystemNotFoundException(@private, CreatePublicException(@public));

        public static EducationSystemPublicException CreatePublicException(string @public)
            => new EducationSystemPublicException(@public);
    }
}