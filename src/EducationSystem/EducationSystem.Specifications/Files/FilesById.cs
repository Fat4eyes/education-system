using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Files
{
    public sealed class FilesById : Specification<DatabaseFile>
    {
        private readonly int _id;

        public FilesById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseFile, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}