using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperQuestionTemplate
    {
        Dictionary<QuestionType, int> GetTemplates(TestSize testSize, List<DatabaseQuestion> questions);
    }
}