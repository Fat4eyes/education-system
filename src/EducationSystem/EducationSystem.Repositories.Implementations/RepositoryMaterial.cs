using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public class RepositoryMaterial : Repository<DatabaseMaterial>, IRepositoryMaterial
    {
        public RepositoryMaterial(DatabaseContext context)
            : base(context) { }

        public async Task<(int Count, List<DatabaseMaterial> Materials)> GetMaterials(FilterMaterial filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return await query.ApplyPaging(filter);
        }
    }
}