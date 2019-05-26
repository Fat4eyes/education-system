using System;
using EducationSystem.Enums;

namespace EducationSystem.Models.Code
{
    public class CodeRunResult
    {
        public string UserOutput { get; set; }

        public string ExpectedOutput { get; set; }

        public CodeRunStatus Status { get; set; }

        public bool Success => IsSuccess();

        private bool IsSuccess()
        {
            bool IsSuccessStatus()
            {
                return Status == CodeRunStatus.Success;
            }

            bool IsSameOutput()
            {
                return string.Equals(UserOutput.Trim(), ExpectedOutput.Trim(), StringComparison.InvariantCulture);
            }

            return IsSuccessStatus() && IsSameOutput();
        }
    }
}