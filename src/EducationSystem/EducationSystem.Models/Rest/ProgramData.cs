using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class ProgramData : Model
    {
        public int ProgramId { get; set; }

        public string Input { get; set; }

        public string ExpectedOutput { get; set; }
    }
}