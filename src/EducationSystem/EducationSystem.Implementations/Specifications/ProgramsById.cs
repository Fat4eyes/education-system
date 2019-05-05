using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class ProgramsById : Specification<DatabaseProgram>
    {
        private readonly int _id;

        public ProgramsById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseProgram, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}