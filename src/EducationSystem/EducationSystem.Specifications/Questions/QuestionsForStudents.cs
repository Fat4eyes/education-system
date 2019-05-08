using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
{
    public sealed class QuestionsForStudents : Specification<DatabaseQuestion>
    {
        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            return x => QuestionTypes.Supported.Contains(x.Type) &&
                        x.Theme.ThemeTests
                            .Select(y => y.Test)
                            .Any(y => y.IsActive);
        }
    }
}