using System.Linq;
using System.Collections.Generic;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Extensions.Source
{
    public static class QueryableExtensions
    {
        public static (int Count, List<T> Items) ApplyPaging<T>(this IQueryable<T> query, Options options)
        {
            var count = query.Count();

            var items = options.All
                ? query.ToList()
                : query
                    .Skip(options.Skip)
                    .Take(options.Take)
                    .ToList();

            return (count, items);
        }
    }
}