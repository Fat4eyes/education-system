using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.Models.Code
{
    public class CodeExecutionResult
    {
        public bool Success => !Errors.Any() && Results.All(x => x.Success);

        public List<string> Errors { get; set; } = new List<string>();

        public List<CodeRunResult> Results { get; set; } = new List<CodeRunResult>();
    }
}