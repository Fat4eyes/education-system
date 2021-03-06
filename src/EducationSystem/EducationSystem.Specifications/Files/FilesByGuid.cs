﻿using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Files
{
    public sealed class FilesByGuid : Specification<DatabaseFile>
    {
        private readonly Guid _guid;

        public FilesByGuid(Guid guid)
        {
            _guid = guid;
        }

        public override Expression<Func<DatabaseFile, bool>> ToExpression()
        {
            return x => x.Guid == _guid;
        }
    }
}