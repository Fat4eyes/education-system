using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Users
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