using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
{
    public sealed class ThemesByStudentId : Specification<DatabaseTheme>
    {
        private readonly int _studentId;

        public ThemesByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            return x => x.Discipline.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}