using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class ProgramData : Model
    {
        public int ProgramId { get; set; }

        public string Input { get; set; }

        public string ExpectedOutput { get; set; }
    }
}