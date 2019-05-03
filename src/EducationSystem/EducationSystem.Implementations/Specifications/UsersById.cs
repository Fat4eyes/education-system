using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class UsersById : Specification<DatabaseUser>
    {
        private readonly int _id;

        public UsersById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseUser, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}