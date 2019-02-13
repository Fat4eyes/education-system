﻿using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudyProfile : IRepositoryReadOnly<DatabaseStudyProfile>
    {
        DatabaseStudyProfile GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options);
    }
}