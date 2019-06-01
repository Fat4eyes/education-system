namespace EducationSystem.Models.Code
{
    public class CodeRunningResult
    {
        public CodeAnalysisResult CodeAnalysisResult { get; set; }

        public CodeExecutionResult CodeExecutionResult { get; set; }

        public int Score { get; set; }
    }
}