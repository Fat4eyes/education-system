﻿using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTheme : IRepositoryReadOnly<DatabaseTheme>
    {
        (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, OptionsTheme options);
    }
}