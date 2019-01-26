using System.Collections.Generic;
using EducationSystem.Managers.Interfaces.Source.Examples;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Examples
{
    [Route("example")]
    public class ExampleController : Controller
    {
        protected IExampleManager ExampleManager { get; }

        public ExampleController(IExampleManager exampleManager)
        {
            ExampleManager = exampleManager;
        }

        [HttpGet]
        [Route("numbers")]
        public IEnumerable<int> GetRandomNumbers()
        {
            return ExampleManager.GetRandomNumbers();
        }
    }
}