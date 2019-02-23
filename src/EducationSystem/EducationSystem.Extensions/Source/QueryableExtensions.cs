namespace EducationSystem.Extensions.Source
{
    using System.Collections.Generic;
    using System.Linq;
    using EducationSystem.Models.Source.Filters;

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