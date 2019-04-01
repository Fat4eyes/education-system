﻿using EducationSystem.Enums;

namespace EducationSystem.Models.Filters
{
    public class FilterTest : Filter
    {
        public string Name { get; set; } = null;

        public int? DisciplineId { get; set; } = null;

        public bool OnlyActive { get; set; } = false;

        public TestType? TestType { get; set; } = null;
    }
}