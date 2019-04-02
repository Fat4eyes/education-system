using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;

namespace EducationSystem.Implementations.Helpers
{
    public class HelperQuestionTemplate : IHelperQuestionTemplate
    {
        public Dictionary<QuestionType, int> GetTemplates(TestSize testSize, List<DatabaseQuestion> questions)
        {
            if (questions.Any(x => x.Type == QuestionType.OpenedManyStrings))
                throw ExceptionHelper.CreateException($"Вопросы с типом '{QuestionType.OpenedManyStrings}' не поддерживаются.");

            if (questions.Count <= (int) testSize)
            {
                return questions
                    .GroupBy(x => x.Type)
                    .Where(x => x.Any())
                    .ToDictionary(x => x.Key, x => x.Count());
            }

            // Шаг для изменения коэффициента.
            var step = 1.0 / (int) testSize;

            // Начальный коэффициент (равновероятный между всеми типами вопросов).
            var rate = 1.0 / questions.GroupBy(x => x.Type).Count();

            // Шаблоны.
            var templates = questions
                .GroupBy(x => x.Type)
                .Select(x => new QuestionTemplate(x, rate))
                .ToList();

            while (true)
            {
                // Для каждого из шаблонов вычисляем количество вопросов (целевых).
                templates.ForEach(x => x.TargetCount = GetTargetCount(x, testSize));

                // Если все шаблоны удовлетворяют условиям, тогда выходим.
                if (templates.All(x => x.Success)) break;

                // Находим шаблон, который удовлетворяет условиям.
                // Он должен быть обязательно (если таких нет, то ошибка в алгоритме).
                var success = templates
                    .OrderByDescending(x => x.Difference)
                    .ThenBy(x => x.Rate)
                    .FirstOrDefault(x => x.Success && x.Rate + step < 1);

                // Находим шаблон, который не удовлетворяет условиям.
                // Он должен быть обязательно (если таких нет, то ошибка в алгоритме).
                var failure = templates.FirstOrDefault(x => x.Success == false && x.Rate - step > 0);

                if (success == null || failure == null)
                    throw ExceptionHelper.CreatePublicException("Не удалось сформировать шаблоны для выборки вопросов.");

                // Пересчитываем коэффициенты.
                success.Rate += step;
                failure.Rate -= step;
            }

            // Проверка шаблонов на необходимость нормализации.
            if (templates.Sum(x => x.TargetCount) <= (int) testSize)
                return templates.ToDictionary(x => x.Type, x => x.TargetCount);

            // Нормализация шаблонов.
            foreach (var template in templates
                .OrderByDescending(x => x.TargetCount)
                .ThenByDescending(x => x.Type))
            {
                if (templates.Sum(x => x.TargetCount) <= (int) testSize)
                    break;

                template.TargetCount--;
            }

            return templates.ToDictionary(x => x.Type, x => x.TargetCount);
        }

        private static int GetTargetCount(QuestionTemplate template, TestSize testSize)
        {
            return (int) Math.Round(template.Rate * (int) testSize, MidpointRounding.AwayFromZero);
        }

        private class QuestionTemplate
        {
            private int SourceCount { get; }

            public QuestionType Type { get; }

            public int TargetCount { get; set; }

            public double Rate { get; set; }

            public bool Success => Difference >= 0;

            public int Difference => SourceCount - TargetCount;

            public QuestionTemplate(IGrouping<QuestionType, DatabaseQuestion> group, double rate)
            {
                Type = group.Key;
                SourceCount = group.Count();

                Rate = rate;
            }
        }
    }
}