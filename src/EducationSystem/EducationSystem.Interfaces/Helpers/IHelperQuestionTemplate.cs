using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Enums;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperQuestionTemplate
    {
        Dictionary<QuestionType, int> GetTemplates(TestSize testSize, List<DatabaseQuestion> questions);
    }
}