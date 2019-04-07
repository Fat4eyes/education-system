using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Builders;

namespace EducationSystem.Implementations.Builders
{
    public sealed class QuestionTemplateBuilder : IQuestionTemplateBuilder
    {
        public Dictionary<QuestionType, int> Build(TestSize testSize, List<DatabaseQuestion> questions)
        {
            ValidateQuestions(questions);

            if (questions.Count <= (int) testSize)
            {
                return questions
                    .GroupBy(x => x.Type)
                    .Where(x => x.Any())
                    .ToDictionary(x => x.Key, x => x.Count());
            }

            var count = (int) Math.Round(
                (int) testSize * 1.0 / questions.GroupBy(x => x.Type).Count(),
                MidpointRounding.AwayFromZero);

            var templates = questions
                .GroupBy(x => x.Type)
                .Select(x => new QuestionTemplate(x) { TargetCount = count })
                .ToList();

            while (true)
            {
                if (templates.All(x => x.Success))
                    break;

                var success = templates
                    .Where(x => x.Success)
                    .OrderByDescending(x => x.Difference)
                    .ThenBy(x => x.TargetCount)
                    .FirstOrDefault();

                var failure = templates.FirstOrDefault(x => x.Success == false);

                if (success == null || failure == null)
                    throw CreateException();

                success.TargetCount++;
                failure.TargetCount--;
            }

            if (templates.Sum(x => x.TargetCount) <= (int) testSize)
                return templates.ToDictionary(x => x.Type, x => x.TargetCount);

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

        private static void ValidateQuestions(IReadOnlyCollection<DatabaseQuestion> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            var questionTypes = new[]
            {
                QuestionType.ClosedOneAnswer,
                QuestionType.ClosedManyAnswers,
                QuestionType.WithProgram
            };

            if (questions.All(x => questionTypes.Contains(x.Type)) == false)
                throw CreateException();
        }

        private static Exception CreateException()
        {
            return ExceptionHelper.CreatePublicException("Не удалось получить шаблоны для выборки вопросов.");
        }

        private class QuestionTemplate
        {
            private int SourceCount { get; }

            public QuestionType Type { get; }

            public int TargetCount { get; set; }

            public bool Success => Difference >= 0;

            public int Difference => SourceCount - TargetCount;

            public QuestionTemplate(IGrouping<QuestionType, DatabaseQuestion> group)
            {
                Type = group.Key;
                SourceCount = group.Count();
            }
        }
    }
}