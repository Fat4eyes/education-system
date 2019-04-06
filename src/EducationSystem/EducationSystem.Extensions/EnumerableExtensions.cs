using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Random = new Random();

        public static bool IsEmpty<T>(this IEnumerable<T> items)
            => items == null || items.Any() == false;

        public static bool IsEmpty<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            => items == null || items.Any(predicate) == false;

        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
            => items?.Any() == true;

        public static bool IsNotEmpty<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            => items?.Any(predicate) == true;

        public static IEnumerable<T> MixItems<T>(this IEnumerable<T> items)
        {
            var elements = items.ToList();

            for (var i = 0; i < elements.Count; i++)
            {
                var element = elements.First();

                elements.RemoveAt(0);

                elements.Insert(Random.Next(elements.Count), element);
            }

            return elements;
        }
    }
}