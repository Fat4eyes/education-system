using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Tests")]
    public class TamerTest : Tamer
    {
        protected IManagerTest ManagerTest { get; }
        protected IManagerTheme ManagerTheme { get; }

        public TamerTest(IManagerTest managerTest, IManagerTheme managerTheme)
        {
            ManagerTest = managerTest;
            ManagerTheme = managerTheme;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTests(OptionsTest options)
        {
            return Json(ManagerTest.GetTests(options));
        }

        [HttpGet("{testId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTest(int testId, OptionsTest options)
        {
            return Json(ManagerTest.GetTestById(testId, options));
        }

        [HttpGet("{testId:int}/Themes")]
        //[Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTest(int testId, OptionsTheme options)
        {
            return Json(ManagerTheme.GetThemesByTestId(testId, options));
        }
    }
}