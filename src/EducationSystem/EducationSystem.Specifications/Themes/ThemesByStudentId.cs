﻿using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Themes
{
    public sealed class ThemesByStudentId : Specification<DatabaseTheme>
    {
        private readonly int _studentId;
        private readonly bool? _passed;

        public ThemesByStudentId(int studentId)
        {
            _studentId = studentId;
        }

        public ThemesByStudentId(int studentId, bool? passed)
        {
            _studentId = studentId;
            _passed = passed;
        }

        public override Expression<Func<DatabaseTheme, bool>> ToExpression()
        {
            if (_passed.HasValue)
            {
                return x => x.Questions.All(y => y.QuestionStudents.Any(z => z.StudentId == _studentId && z.Passed)) == _passed &&
                            x.Discipline.StudyProfiles
                                .Any(a => a.StudyProfile.StudyPlans
                                .Any(b => b.Groups
                                .Any(c => c.GroupStudents
                                .Any(d => d.StudentId == _studentId))));
            }

            return x => x.Discipline.StudyProfiles
                .Any(a => a.StudyProfile.StudyPlans
                .Any(b => b.Groups
                .Any(c => c.GroupStudents
                .Any(d => d.StudentId == _studentId))));
        }
    }
}