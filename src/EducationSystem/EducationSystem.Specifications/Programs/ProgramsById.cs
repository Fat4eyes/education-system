using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Programs
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