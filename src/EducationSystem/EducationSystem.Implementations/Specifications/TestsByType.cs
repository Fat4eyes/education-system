﻿using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class TestsByType : Specification<DatabaseTest>
    {
        private readonly TestType? _testType;

        public TestsByType(TestType? testType)
        {
            _testType = testType;
        }

        public override Expression<Func<DatabaseTest, bool>> ToExpression()
        {
            if (_testType.HasValue)
                return x => x.Type == _testType;

            return x => true;
        }
    }
}