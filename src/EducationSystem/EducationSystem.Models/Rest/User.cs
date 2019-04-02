﻿using System.Collections.Generic;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class User : Model
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public List<Role> Roles { get; set; }
    }
}