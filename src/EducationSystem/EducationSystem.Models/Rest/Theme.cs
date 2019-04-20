using System.Collections.Generic;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Theme : Model
    {
        public string Name { get; set; }

        public int DisciplineId { get; set; }

        public int? Order { get; set; }

        public List<Question> Questions { get; set; }
    }
}