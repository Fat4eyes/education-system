using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class TestsForStudent : Specification<DatabaseTest>
    {
        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            return x => x.IsActive == 1 && x.TestThemes.Any(z => z.Theme.Questions.Any());
        }
    }
}