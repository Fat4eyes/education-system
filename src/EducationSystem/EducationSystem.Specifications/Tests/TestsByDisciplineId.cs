using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsByDisciplineId : Specification<DatabaseTest>
    {
        private readonly int? _disciplineId;

        public TestsByDisciplineId(int? disciplineId)
        {
            _disciplineId = disciplineId;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            if (_disciplineId.HasValue)
                return x => x.DisciplineId == _disciplineId;

            return x => true;
        }
    }
}