using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
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