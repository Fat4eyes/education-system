using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryQuestion : IRepositoryReadOnly<DatabaseQuestion>
    {
        (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, OptionsQuestion options);
    }
}