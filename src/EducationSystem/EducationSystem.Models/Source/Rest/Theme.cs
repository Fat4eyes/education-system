using System.Collections.Generic;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Theme : Model
    {
        public string Name { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public List<Question> Questions { get; set; }
    }
}