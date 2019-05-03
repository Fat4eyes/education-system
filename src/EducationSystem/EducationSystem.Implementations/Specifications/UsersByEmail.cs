using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
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
            return x => string.Equals(x.Email, _email, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}