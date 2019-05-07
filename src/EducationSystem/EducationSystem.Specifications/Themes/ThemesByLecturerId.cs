using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
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