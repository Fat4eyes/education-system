using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;

namespace EducationSystem.Helpers.Implementations.Source
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
                .Select(x => new QuestionTemplate(rate, x.Key, x.Count()))
                .ToList();

            while (true)
            {
                foreach (var template in templates)
                {
                    // Для каждого из шаблонов вычисляем количество вопросов (целевых).
                    template.TargetCount = (int) Math.Round(template.Rate * (int) testSize, MidpointRounding.AwayFromZero);
                }

                // Если все шаблоны удовлетворяют условиям, тогда выходим.
                if (templates.All(x => x.Success)) break;

                // Находим шаблон, который удовлетворяет условиям.
                // Он должен быть обязательно (если таких нет, то ошибка в алгоритме).
                var success = templates
                    .OrderByDescending(x => x.Difference)
                    .ThenBy(x => x.Rate)
                    .FirstOrDefault(x => x.Success && x.Rate + step < 1) ??
                        throw ExceptionHelper.CreatePublicException("Не удалось сформировать шаблоны для выборки вопросов.");

                // Находим шаблон, который не удовлетворяет условиям.
                // Он должен быть обязательно (если таких нет, то ошибка в алгоритме).
                var failure = templates.FirstOrDefault(x => x.Success == false && x.Rate - step > 0) ??
                    throw ExceptionHelper.CreatePublicException("Не удалось сформировать шаблоны для выборки вопросов.");

                // Пересчитываем коэффициенты.
                success.Rate += step;
                failure.Rate -= step;
            }

            // Проверка шаблонов на необходимость нормализации.
            if (templates.Sum(x => x.TargetCount) <= (int) testSize)
                return templates.ToDictionary(x => x.QuestionType, x => x.TargetCount);

            // Нормализация шаблонов.
            foreach (var data in templates
                .OrderByDescending(x => x.TargetCount)
                .ThenByDescending(x => x.GetPosition()))
            {
                if (templates.Sum(x => x.TargetCount) <= (int) testSize)
                    break;

                data.TargetCount--;
            }

            return templates.ToDictionary(x => x.QuestionType, x => x.TargetCount);
        }

        private sealed class QuestionTemplate
        {
            private int SourceCount { get; }

            public QuestionType QuestionType { get; }

            public int TargetCount { get; set; }

            public double Rate { get; set; }

            public bool Success => Difference >= 0;

            public int Difference => SourceCount - TargetCount;

            public QuestionTemplate(double rate, QuestionType questionType, int sourceCount)
            {
                Rate = rate;
                QuestionType = questionType;
                SourceCount = sourceCount;
            }

            public int GetPosition()
            {
                switch (QuestionType)
                {
                    case QuestionType.ClosedOneAnswer:
                        return 1;
                    case QuestionType.ClosedManyAnswers:
                        return 2;
                    case QuestionType.WithProgram:
                        return 3;
                    case QuestionType.OpenedOneString:
                        return 4;
                }

                throw ExceptionHelper.CreatePublicException("Не удалось сформировать шаблоны для выборки вопросов.");
            }
        }
    }
}