using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsById : Specification<DatabaseTest>
    {
        private readonly int _id;

        public TestsById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}