using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Materials
{
    public sealed class MaterialsByOwnerId : Specification<DatabaseMaterial>
    {
        private readonly int _ownerId;

        public MaterialsByOwnerId(int ownerId)
        {
            _ownerId = ownerId;
        }

        public override Expression<Func<DatabaseMaterial, bool>> ToExpression()
        {
            return x => x.OwnerId == _ownerId;
        }
    }
}