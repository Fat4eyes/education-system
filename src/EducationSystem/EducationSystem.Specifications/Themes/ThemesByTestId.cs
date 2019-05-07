using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
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
            if (_testId.HasValue)
                return x => x.ThemeTests.Any(y => y.TestId == _testId);

            return x => true;
        }
    }
}