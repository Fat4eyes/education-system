using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Disciplines")]
    public class TamerDiscipline : Tamer
    {
        protected IManagerTest ManagerTest { get; }
        protected IManagerTheme ManagerTheme { get; }
        protected IManagerDiscipline ManagerDiscipline { get; }

        public TamerDiscipline(
            IManagerTest managerTest,
            IManagerTheme managerTheme,
            IManagerDiscipline managerDiscipline)
        {
            ManagerDiscipline = managerDiscipline;
            ManagerTest = managerTest;
            ManagerTheme = managerTheme;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetDisciplines(OptionsDiscipline options)
        {
            return Json(ManagerDiscipline.GetDisciplines(options));
        }

        [HttpGet("{disciplineId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetDiscipline(int disciplineId, OptionsDiscipline options)
        {
            return Json(ManagerDiscipline.GetDisciplineById(disciplineId, options));
        }

        [HttpGet("{disciplineId:int}/Tests")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetDisciplineTests(int disciplineId, OptionsTest options)
        {
            return Json(ManagerTest.GetTestsByDisciplineId(disciplineId, options));
        }

        [HttpGet("{disciplineId:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetDisciplineThemes(int disciplineId, OptionsTheme options)
        {
            return Json(ManagerTheme.GetThemesByDisciplineId(disciplineId, options));
        }
    }
}