using System.Collections.Generic;
using EducationSystem.Enums;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Test : Model
    {
        public string Subject { get; set; }

        public bool IsActive { get; set; }

        public int DisciplineId { get; set; }

        public int? TotalTime { get; set; }

        public int? Attempts { get; set; }

        public TestType? Type { get; set; }

        public int? IsRandom { get; set; }

        public List<Theme> Themes { get; set; }
    }
}