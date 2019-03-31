using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;

namespace EducationSystem.Helpers.Interfaces.Source
{
    public interface IHelperQuestionTemplate
    {
        Dictionary<QuestionType, int> GetTemplates(TestSize testSize, List<DatabaseQuestion> questions);
    }
}