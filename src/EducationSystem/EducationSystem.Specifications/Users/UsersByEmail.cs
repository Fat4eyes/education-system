using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Users
{
    public sealed class UsersByEmail : Specification<DatabaseUser>
    {
        private readonly string _email;

        public UsersByEmail(string email)
        {
            _email = email;
        }

        public override Expression<Func<DatabaseUser, bool>> ToExpression()
        {
            var upper = _email.ToUpper();

            return x => x.Email.ToUpper() == upper;
        }
    }
}