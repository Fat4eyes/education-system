using System;
using EducationSystem.Enums;

namespace EducationSystem.Models.Code
{
    public class CodeRunResult
    {
        public string UserOutput { get; set; }

        public string ExpectedOutput { get; set; }

        public CodeRunStatus Stutus { get; set; }

        public bool Success =>
            Stutus == CodeRunStatus.Success &&
            string.Equals(UserOutput, ExpectedOutput, StringComparison.InvariantCulture);
    }
}