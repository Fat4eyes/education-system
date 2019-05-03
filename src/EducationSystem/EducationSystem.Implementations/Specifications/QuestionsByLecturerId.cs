using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class QuestionsByLecturerId : Specification<DatabaseQuestion>
    {
        private readonly int _lecturerId;

        public QuestionsByLecturerId(int lecturerId)
        {
            _lecturerId = lecturerId;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            return x => x.Theme.Discipline.Lecturers.Any(y => y.LecturerId == _lecturerId);
        }
    }
}