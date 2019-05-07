using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Programs
{
    public sealed class ProgramsByStudentId : Specification<DatabaseProgram>
    {
        private readonly int _studentId;

        public ProgramsByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public override Expression<Func<DatabaseProgram, bool>> ToExpression()
        {
            return x => x.Question.Theme.Discipline.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}