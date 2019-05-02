using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> items)
            => items == null || items.Any() == false;

        public static bool IsEmpty<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            => items == null || items.Any(predicate) == false;

        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
            => items?.Any() == true;

        public static bool IsNotEmpty<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            => items?.Any(predicate) == true;

        public static IEnumerable<string> ToLower(this IEnumerable<string> items)
            => items.Select(x => x.ToLower());

        public static IEnumerable<string> ToLowerInvariant(this IEnumerable<string> items)
            => items.Select(x => x.ToLowerInvariant());

        public static IEnumerable<string> ToUpper(this IEnumerable<string> items)
            => items.Select(x => x.ToUpper());

        public static IEnumerable<string> ToUpperInvariant(this IEnumerable<string> items)
            => items.Select(x => x.ToUpperInvariant());

        public static async Task<bool> AllAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
        {
            foreach (var item in source)
            {
                if (await predicate(item) == false)
                    return false;
            }

            return true;
        }
    }
}