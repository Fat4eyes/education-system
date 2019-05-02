using System.Linq;
using EducationSystem.Database.Models;

namespace EducationSystem.Models.Datas
{
    public class TestData
    {
        public int ThemesCount { get; set; }

        public int QuestionsCount { get; set; }

        public int PassedThemesCount { get; set; }

        public int PassedQuestionsCount { get; set; }

        public static TestData Create(DatabaseTheme[] themes, int studentId)
        {
            var questions = themes
                .SelectMany(x => x.Questions)
                .ToArray();

            var passedThemesCount = themes
                .Count(x => x.Questions
                    .All(y => y.QuestionStudents
                        .Any(z => z.StudentId == studentId && z.Passed)));

            var passedQuestionsCount = questions
                .Count(x => x.QuestionStudents
                    .Any(y => y.StudentId == studentId && y.Passed));

            return new TestData
            {
                ThemesCount = themes.Length,
                QuestionsCount = questions.Length,
                PassedThemesCount = passedThemesCount,
                PassedQuestionsCount = passedQuestionsCount
            };
        }
    }
}