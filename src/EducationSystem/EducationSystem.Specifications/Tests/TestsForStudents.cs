﻿using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Tests
{
    public sealed class TestsForStudents : Specification<DatabaseTest>
    {
        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            var types = QuestionTypeHelper.GetSupportedTypes();

            return x => x.IsActive &&
                        x.TestThemes
                            .Select(y => y.Theme)
                            .SelectMany(y => y.Questions)
                            .Any(z => types.Contains(z.Type));
        }
    }
}