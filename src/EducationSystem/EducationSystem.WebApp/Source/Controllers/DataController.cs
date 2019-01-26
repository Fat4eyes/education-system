using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers
{
    [Route("data")]
    public class DataController : Controller
    {
        protected static Random Random { get; } = new Random();

        [HttpGet]
        [Route("numbers")]
        public IEnumerable<int> GetRandomNumbers()
        {
            return Enumerable.Range(0, 10)
                .Select(x => Random.Next(100))
                .ToList();
        }
    }
}