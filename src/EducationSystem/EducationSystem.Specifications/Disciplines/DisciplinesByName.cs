using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.Disciplines
{
    public sealed class DisciplinesByName : Specification<DatabaseDiscipline>
    {
        private readonly string _name;

        public DisciplinesByName(string name)
        {
            _name = name;
        }

        public override Expression<Func<DatabaseDiscipline, bool>> ToExpression()
        {
            if (string.IsNullOrWhiteSpace(_name))
                return x => true;

            return x => x.Name.Contains(_name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}