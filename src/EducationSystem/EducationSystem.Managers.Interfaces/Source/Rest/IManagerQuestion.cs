using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerQuestion
    {
        PagedData<Question> GetQuestionsByThemeId(int themeId, OptionsQuestion options);
    }
}