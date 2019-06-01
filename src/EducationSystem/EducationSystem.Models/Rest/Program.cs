using System.Collections.Generic;
using EducationSystem.Enums;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Program : Model
    {
        public int QuestionId { get; set; }

        public string Template { get; set; }

        public LanguageType? LanguageType { get; set; }

        public int? TimeLimit { get; set; }

        public int? MemoryLimit { get; set; }

        public string Source { get; set; }

        public List<ProgramData> ProgramDatas { get; set; }

        public CodeRunningResult CodeRunningResult { get; set; }

        public Program SetCodeRunningResult(CodeRunningResult result)
        {
            CodeRunningResult = result;
            return this;
        }
    }
}