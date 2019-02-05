using System.Collections.Generic;

namespace EducationSystem.Models.Source.Rest
{
    public class UserWithTestResults : UserWithGroupAndStudyPlan
    {
        public List<TestResult> TestResults { get; set; }
    }
}