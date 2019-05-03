using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class DisciplinesByStudentId : Specification<DatabaseDiscipline>
    {
        private readonly int _studentId;

        public DisciplinesByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            return x => x.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}