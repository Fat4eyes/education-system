using System.Collections.Generic;
using EducationSystem.Models.Datas;
using EducationSystem.Models.Rest;

namespace EducationSystem.Models
{
    public class TestExecution
    {
        public TestData TestData { get; set; }

        public List<Question> Questions { get; set; }

        public TestExecution(TestData testData, List<Question> questions)
        {
            TestData = testData;
            Questions = questions;
        }
    }
}