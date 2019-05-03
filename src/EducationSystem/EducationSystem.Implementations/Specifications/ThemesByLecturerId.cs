using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class ThemesByLecturerId : Specification<DatabaseTheme>
    {
        private readonly int _lecturerId;

        public ThemesByLecturerId(int lecturerId)
        {
            _lecturerId = lecturerId;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => x.Discipline.Lecturers.Any(y => y.LecturerId == _lecturerId);
        }
    }
}