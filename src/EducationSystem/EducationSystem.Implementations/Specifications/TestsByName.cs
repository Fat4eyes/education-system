using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
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

            return x => x.Subject.Contains(_name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}