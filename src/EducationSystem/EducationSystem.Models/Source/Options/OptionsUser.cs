﻿namespace EducationSystem.Models.Source.Options
{
    public class OptionsUser : Options
    {
        public bool WithRoles { get; set; } = false;

        public static OptionsUser IncludeRoles =>
            new OptionsUser { WithRoles = true };
    }
}