namespace EducationSystem.Models.Datas
{
    public class TestData
    {
        public int? ThemesCount { get; set; }

        public int? QuestionsCount { get; set; }

        public TestData(int? themesCount, int? questionsCount)
        {
            ThemesCount = themesCount;
            QuestionsCount = questionsCount;
        }
    }
}