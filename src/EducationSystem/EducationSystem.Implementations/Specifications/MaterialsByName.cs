using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications.Basics;

namespace EducationSystem.Implementations.Specifications
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

            return x => x.Name.Contains(_name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}