using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Files
{
    public sealed class FilesByIds : Specification<DatabaseFile>
    {
        private readonly int[] _ids;

        public FilesByIds(int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<DatabaseFile, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}