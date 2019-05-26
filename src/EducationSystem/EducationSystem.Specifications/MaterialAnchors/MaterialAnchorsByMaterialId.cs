using System;
using System.Linq.Expressions;
using EducationSystem.Database.Models;
using EducationSystem.Specifications.Basics;

namespace EducationSystem.Specifications.MaterialAnchors
{
    public sealed class MaterialAnchorsByMaterialId : Specification<DatabaseMaterialAnchor>
    {
        private readonly int _materialId;

        public MaterialAnchorsByMaterialId(int materialId)
        {
            _materialId = materialId;
        }

        public override Expression<Func<DatabaseMaterialAnchor, bool>> ToExpression()
        {
            return x => x.MaterialId == _materialId;
        }
    }
}