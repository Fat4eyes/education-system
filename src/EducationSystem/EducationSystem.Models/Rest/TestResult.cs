using System;
using System.Collections.Generic;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class TestResult : Model
    {
        public int Mark { get; set; }

        public DateTime DateTime { get; set; }

        public int TestId { get; set; }

        public int UserId { get; set; }

        public Test Test { get; set; }

        public List<GivenAnswer> GivenAnswers { get; set; }
    }
}