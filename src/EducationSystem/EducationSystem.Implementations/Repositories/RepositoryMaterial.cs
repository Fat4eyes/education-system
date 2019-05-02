using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public class RepositoryMaterial : Repository<DatabaseMaterial>, IRepositoryMaterial
    {
        public RepositoryMaterial(DatabaseContext context) : base(context) { }

        public Task<(int Count, List<DatabaseMaterial> Materials)> GetMaterialsAsync(FilterMaterial filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseMaterial> Materials)> GetUserMaterialsAsync(int userId, FilterMaterial filter)
        {
            var query = AsQueryable()
                .Where(x => x.OwnerId == userId);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<DatabaseMaterial> GetUserMaterialAsync(int id, int userId)
        {
            return AsQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(x => x.OwnerId == userId);
        }
    }
}