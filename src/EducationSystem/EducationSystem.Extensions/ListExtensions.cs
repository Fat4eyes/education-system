using System;
using System.Collections.Generic;

namespace EducationSystem.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Execute<T>(this List<T> items, Action<T> action)
        {
            items.ForEach(action);

            return items;
        }
    }
}