using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.Models.Code
{
    public class CodeExecutionResponse
    {
        public bool Success => !Errors.Any() && Results.All(x => x.Success);

        public List<string> Errors { get; set; }

        public List<CodeRunResult> Results { get; set; } = new List<CodeRunResult>();
    }
}