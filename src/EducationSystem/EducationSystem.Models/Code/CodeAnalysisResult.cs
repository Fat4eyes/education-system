using System.Collections.Generic;

namespace EducationSystem.Models.Code
{
    public class CodeAnalysisResult
    {
        public bool Success { get; set; }

        public List<CodeAnalysisMessage> Messages { get; set; } = new List<CodeAnalysisMessage>();
    }
}