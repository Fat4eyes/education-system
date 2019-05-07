using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Files
{
    public sealed class FilesByOwnerId : Specification<DatabaseFile>
    {
        private readonly int _ownerId;

        public FilesByOwnerId(int ownerId)
        {
            _ownerId = ownerId;
        }

        public override Expression<Func<DatabaseFile, bool>> ToExpression()
        {
            return x => x.OwnerId == _ownerId;
        }
    }
}