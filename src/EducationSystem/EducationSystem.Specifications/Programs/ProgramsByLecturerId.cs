﻿using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Programs
{
    public sealed class ProgramsByLecturerId : Specification<DatabaseProgram>
    {
        private readonly int _lecturerId;

        public ProgramsByLecturerId(int lecturerId)
        {
            _lecturerId = lecturerId;
        }

        public override Expression<Func<DatabaseProgram, bool>> ToExpression()
        {
            return x => x.Question.Theme.Discipline.Lecturers.Any(y => y.LecturerId == _lecturerId);
        }
    }
}