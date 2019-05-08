using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Disciplines
{
    public sealed class DisciplinesForStudents : Specification<DatabaseDiscipline>
    {
        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            return x => x.Tests
                .Where(y => y.IsActive)
                .SelectMany(y => y.TestThemes)
                .Select(y => y.Theme)
                .SelectMany(y => y.Questions)
                .Any(y => QuestionTypes.Supported.Contains(y.Type));
        }
    }
}