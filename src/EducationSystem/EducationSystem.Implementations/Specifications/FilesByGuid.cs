using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
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
            return x => string.Equals(x.Guid, _guid.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}