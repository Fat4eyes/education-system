using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Questions
{
    public sealed class QuestionsByStudentId : Specification<DatabaseQuestion>
    {
        private readonly int _studentId;

        public QuestionsByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            return x => x.Theme.Discipline.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}