using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryQuestion : IRepositoryReadOnly<DatabaseQuestion>
    {
        (int Count, List<DatabaseQuestion> Questions) GetQuestionsByThemeId(int themeId, Filter filter);
    }
}