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

        public static async Task<bool> AllAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source)
            {
                if (await predicate(item) == false)
                    return false;
            }

            return true;
        }

        public static async Task<bool> AllAsync<TSource>(this IEnumerable<Task<TSource>> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source)
            {
                if (!predicate(await item))
                    return false;
            }

            return true;
        }
    }
}