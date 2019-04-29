using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<(int Count, List<T> Items)> ApplyPaging<T>(this IQueryable<T> query, Filter filter)
        {
            var count = query.Count();

            var items = filter.All
                ? query.ToList()
                : await query
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            return (count, items);
        }
    }
}