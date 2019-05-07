using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsByLecturerId : Specification<DatabaseTest>
    {
        private readonly int _lecturerId;

        public TestsByLecturerId(int lecturerId)
        {
            _lecturerId = lecturerId;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            return x => x.Discipline.Lecturers.Any(y => y.LecturerId == _lecturerId);
        }
    }
}