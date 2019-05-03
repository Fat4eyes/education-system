using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class DisciplinesByLecturerId : Specification<DatabaseDiscipline>
    {
        private readonly int _lecturerId;

        public DisciplinesByLecturerId(int lecturerId)
        {
            _lecturerId = lecturerId;
        }

        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            return x => x.Lecturers.Any(y => y.LecturerId == _lecturerId);
        }
    }
}