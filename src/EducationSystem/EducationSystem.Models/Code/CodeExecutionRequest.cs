using EducationSystem.Models.Rest;

namespace EducationSystem.Models.Code
{
    public class CodeExecutionRequest
    {
        public string Source { get; set; }

        public Program Program { get; set; }
    }
}