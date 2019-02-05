using System;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class TestResult : Model
    {
        public Test Test { get; set; }

        public int Attempt { get; set; }

        public int Mark { get; set; }

        public DateTime DateTime { get; set; }
    }
}