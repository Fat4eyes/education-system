using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsByName : Specification<DatabaseTest>
    {
        private readonly string _name;

        public TestsByName(string name)
        {
            _name = name;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            if (string.IsNullOrWhiteSpace(_name))
                return x => true;

            return x => x.Subject.ToUpper().Contains(_name.ToUpper());
        }
    }
}