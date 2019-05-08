using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
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
            return x => x.IsActive == _isActive;
        }
    }
}