namespace EducationSystem.Models.Code
{
    public class CodeAnalysisMessage
    {
        public bool IsError { get; set; }

        public bool IsWarning { get; set; }

        public string Text { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }
    }
}