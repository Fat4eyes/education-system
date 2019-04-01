using System.Collections.Generic;

namespace EducationSystem.Models
{
    public class PagedData<T>
    {
        public List<T> Items { get; set; }

        public int Count { get; set; }

        public PagedData(List<T> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}