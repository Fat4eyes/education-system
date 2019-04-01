using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Enums;

namespace EducationSystem.Tests
{
    internal static class Creator
    {
        public static DatabaseTheme CreateTheme(params DatabaseQuestion[] questions)
        {
            var theme = new DatabaseTheme {
                Name = "Theme",
                Questions = new List<DatabaseQuestion>()
            };

            foreach (var question in questions)
                theme.Questions.Add(question);

            return theme;
        }

        public static DatabaseTest CreateTest(params DatabaseTheme[] themes)
        {
            var test = new DatabaseTest {
                Subject = "Test",
                TestThemes = new List<DatabaseTestTheme>()
            };

            foreach (var theme in themes)
                test.TestThemes.Add(new DatabaseTestTheme { Theme = theme });

            return test;
        }

        public static DatabaseTest CreateActiveTest(params DatabaseTheme[] themes)
        {
            var test = CreateTest(themes);

            test.IsActive = 1;

            return test;
        }

        public static DatabaseQuestion CreateQuestion(params DatabaseAnswer[] answers)
        {
            var question = new DatabaseQuestion {
                Answers = new List<DatabaseAnswer>(),
                Type = QuestionType.ClosedManyAnswers
            };

            foreach (var answer in answers)
                question.Answers.Add(answer);

            return question;
        }

        public static DatabaseAnswer CreateAnswer()
        {
            return new DatabaseAnswer();
        }

        public static DatabaseAnswer CreateRightAnswer()
        {
            var answer = CreateAnswer();

            answer.IsRight = 1;

            return answer;
        }
    }
}