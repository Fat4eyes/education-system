using System.Collections.Generic;

namespace EducationSystem.WebApp.Source.Models
{
    public class Cors
    {
        public string Policy { get; set; }

        public List<string> Origins { get; set; }
    }
}