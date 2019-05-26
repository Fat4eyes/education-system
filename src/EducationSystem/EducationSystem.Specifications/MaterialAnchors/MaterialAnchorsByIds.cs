using System;
using System.Linq;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.MaterialAnchors
{
    public sealed class MaterialAnchorsByIds : Specification<DatabaseMaterialAnchor>
    {
        private readonly int[] _ids;

        public MaterialAnchorsByIds(int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<DatabaseMaterialAnchor, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}