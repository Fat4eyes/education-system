using System;
using EducationSystem.Enums;

namespace EducationSystem.Models.Code
{
    public class CodeRunResult
    {
        public string UserOutput { get; set; }

        public string ExpectedOutput { get; set; }

        public CodeRunStatus Status { get; set; }

        public bool Success =>
            Status == CodeRunStatus.Success &&
            string.Equals(UserOutput, ExpectedOutput, StringComparison.InvariantCulture);
    }
}