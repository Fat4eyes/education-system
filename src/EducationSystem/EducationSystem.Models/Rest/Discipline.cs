using System.Collections.Generic;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Discipline : Model
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public List<Test> Tests { get; set; }

        public List<Theme> Themes { get; set; }

        public List<User> Lecturers { get; set; }
    }
}