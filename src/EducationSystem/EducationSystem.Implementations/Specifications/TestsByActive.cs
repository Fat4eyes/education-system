using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class TestsByActive : Specification<DatabaseTest>
    {
        private readonly bool _isActive;

        public TestsByActive(bool isActive)
        {
            _isActive = isActive;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            var isActive = _isActive ? 1 : 0;

            return x => x.IsActive == isActive;
        }
    }
}