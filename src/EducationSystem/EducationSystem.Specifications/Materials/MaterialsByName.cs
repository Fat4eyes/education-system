using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Materials
{
    public sealed class MaterialsByName : Specification<DatabaseMaterial>
    {
        private readonly string _name;

        public MaterialsByName(string name)
        {
            _name = name;
        }

        public override Expression<Func<DatabaseMaterial, bool>> ToExpression()
        {
            if (string.IsNullOrWhiteSpace(_name))
                return x => true;

            var upper = _name.ToUpper();

            return x => x.Name.ToUpper().Contains(upper);
        }
    }
}