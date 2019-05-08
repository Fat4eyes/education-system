using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
{
    public sealed class QuestionsByTestId : Specification<DatabaseQuestion>
    {
        private readonly int? _testId;

        public QuestionsByTestId(int? testId)
        {
            _testId = testId;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            if (_testId.HasValue)
            {
                return x => x.Theme.ThemeTests.Any(y => y.TestId == _testId);
            }

            return x => true;
        }
    }
}