using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
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