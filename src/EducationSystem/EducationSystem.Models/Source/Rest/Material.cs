using System.Collections.Generic;
using EducationSystem.Models.Source.Files;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Material : Model
    {
        public string Name { get; set; }

        public string Template { get; set; }

        public List<File> Files { get; set; }
    }
}