using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsByIds : Specification<DatabaseTest>
    {
        private readonly int[] _ids;

        public TestsByIds(int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}