using EducationSystem.Enums;

namespace EducationSystem.Models.Options
{
    public class OptionsTestExecution : Options
    {
        public TestSize TestSize { get; set; } = TestSize.S;
    }
}