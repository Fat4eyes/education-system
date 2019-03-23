using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryMaterial : Repository<DatabaseMaterial>, IRepositoryMaterial
    {
        public RepositoryMaterial(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseMaterial> Materials) GetMaterials(FilterMaterial filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }
    }
}