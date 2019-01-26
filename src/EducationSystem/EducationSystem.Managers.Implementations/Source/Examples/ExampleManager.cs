using System;
using System.Linq;
using System.Collections.Generic;
using EducationSystem.Managers.Interfaces.Source.Examples;

namespace EducationSystem.Managers.Implementations.Source.Examples
{
    public class ExampleManager : IExampleManager
    {
        protected static Random Random { get; } = new Random();

        public List<int> GetRandomNumbers()
        {
            return Enumerable.Range(0, 10)
                .Select(x => Random.Next(100))
                .ToList();
        }
    }
}