﻿using EducationSystem.Models.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerStudyProfile
    {
        StudyProfile GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options);
    }
}