using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Files
{
    public sealed class FilesByType : Specification<DatabaseFile>
    {
        private readonly FileType? _type;

        public FilesByType(FileType? type)
        {
            _type = type;
        }

        public override Expression<Func<DatabaseFile, bool>> ToExpression()
        {
            if (_type.HasValue)
                return x => x.Type == _type;

            return x => true;
        }
    }
}