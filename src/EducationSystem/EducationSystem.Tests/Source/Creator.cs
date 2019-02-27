using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Tests.Source
{
    public static class Creator
    {
        public static DatabaseTheme CreateTheme()
        {
            return new DatabaseTheme { Name = "Theme" };
        }

        public static DatabaseTheme CreateThemeWithQuestions()
        {
            return new DatabaseTheme
            {
                Name = "Theme",
                Questions = new List<DatabaseQuestion>
                {
                    new DatabaseQuestion(),
                    new DatabaseQuestion(),
                    new DatabaseQuestion()
                }
            };
        }

        public static DatabaseTest CreateTest(bool isActive)
        {
            return new DatabaseTest
            {
                Subject = "Test",
                IsActive = isActive ? 1 : 0,
                TestThemes = new List<DatabaseTestTheme> {
                    new DatabaseTestTheme { Theme = CreateThemeWithQuestions() }
                }
            };
        }
    }
}