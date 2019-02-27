using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.Extensions.Source
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
    }
}