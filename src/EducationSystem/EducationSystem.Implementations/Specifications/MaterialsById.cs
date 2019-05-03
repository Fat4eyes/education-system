using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
{
    public sealed class MaterialsById : Specification<DatabaseMaterial>
    {
        private readonly int _id;

        public MaterialsById(int id)
        {
            _id = id;
        }

        public override Expression<Func<DatabaseMaterial, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}