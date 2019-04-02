using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Enums;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerQuestionTemplate
    {
        Dictionary<QuestionType, int> CreateTemplates(TestSize testSize, List<DatabaseQuestion> questions);
    }
}