using System.Collections.Generic;
using EducationSystem.Models.Files;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Material : Model
    {
        public string Name { get; set; }

        public string Template { get; set; }

        public int? OwnerId { get; set; }

        public List<Document> Files { get; set; }

        public Material Format()
        {
            Name = Name.Trim();
            Template = Template.Trim();

            return this;
        }
    }
}