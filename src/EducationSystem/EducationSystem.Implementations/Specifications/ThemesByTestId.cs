using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class ThemesByTestId : Specification<DatabaseTheme>
    {
        private readonly int? _testId;

        public ThemesByTestId(int? testId)
        {
            _testId = testId;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => x.ThemeTests.Any(y => y.TestId == _testId);
        }
    }
}