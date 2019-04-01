using System.Collections.Generic;
using System.Linq;
using EducationSystem.Models.Filters;

namespace EducationSystem.Extensions
{
    public static class QueryableExtensions
    {
        public static (int Count, List<T> Items) ApplyPaging<T>(this IQueryable<T> query, Filter filter)
        {
            var count = query.Count();

            var items = filter.All
                ? query.ToList()
                : query
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToList();

            return (count, items);
        }
    }
}