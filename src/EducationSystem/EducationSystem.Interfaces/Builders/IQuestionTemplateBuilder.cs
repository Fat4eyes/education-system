using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Enums;

namespace EducationSystem.Interfaces.Builders
{
    public interface IQuestionTemplateBuilder
    {
        Dictionary<QuestionType, int> Build(TestSize testSize, List<DatabaseQuestion> questions);
    }
}