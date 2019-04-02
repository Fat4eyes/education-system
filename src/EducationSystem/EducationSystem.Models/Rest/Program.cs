using System.Collections.Generic;
using EducationSystem.Enums;
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

        public List<ProgramData> ProgramDatas { get; set; }
    }
}