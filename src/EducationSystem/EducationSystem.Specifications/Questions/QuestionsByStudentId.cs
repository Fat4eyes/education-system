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
        private readonly bool? _passed;

        public QuestionsByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public QuestionsByStudentId(int studentId, bool? passed) : this(studentId)
        {
            _passed = passed;
        }

        public override Expression<Func<DatabaseQuestion, bool>> ToExpression()
        {
            if (_passed == true)
            {
                return x =>
                    x.QuestionStudents.Any(y => y.StudentId == _studentId && y.Passed) &&
                    x.Theme.Discipline.StudyProfiles
                        .Any(a => a.StudyProfile.StudyPlans
                        .Any(b => b.Groups
                        .Any(c => c.GroupStudents
                        .Any(d => d.StudentId == _studentId))));
            }

            if (_passed == false)
            {
                return x =>
                    x.QuestionStudents.Any(y => y.StudentId == _studentId && y.Passed) == false &&
                    x.Theme.Discipline.StudyProfiles
                        .Any(a => a.StudyProfile.StudyPlans
                        .Any(b => b.Groups
                        .Any(c => c.GroupStudents
                        .Any(d => d.StudentId == _studentId))));
            }

            return x => x.Theme.Discipline.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}