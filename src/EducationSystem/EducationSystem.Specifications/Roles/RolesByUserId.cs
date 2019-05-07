using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Roles
{
    public sealed class RolesByUserId : Specification<DatabaseRole>
    {
        private readonly int _userId;

        public RolesByUserId(int userId)
        {
            _userId = userId;
        }

        public override Expression<Func<DatabaseRole, bool>> ToExpression()
        {
            return x => x.RoleUsers.Any(y => y.User.Id == _userId);
        }
    }
}