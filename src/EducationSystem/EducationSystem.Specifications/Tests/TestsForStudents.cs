using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsForStudents : Specification<DatabaseTest>
    {
        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            return x => x.IsActive &&
                        x.TestThemes
                            .Select(y => y.Theme)
                            .SelectMany(y => y.Questions)
                            .Any(z => QuestionTypes.Supported.Contains(z.Type));
        }
    }
}