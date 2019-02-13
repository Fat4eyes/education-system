using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Disciplines")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
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
        public IActionResult GetDisciplines(OptionsDiscipline options) =>
            Json(ManagerDiscipline.GetDisciplines(options));

        [HttpGet("{disciplineId:int}")]
        public IActionResult GetDiscipline(int disciplineId, OptionsDiscipline options) =>
            Json(ManagerDiscipline.GetDisciplineById(disciplineId, options));

        [HttpGet("{disciplineId:int}/Tests")]
        public IActionResult GetDisciplineTests(int disciplineId, OptionsTest options) =>
            Json(ManagerTest.GetTestsByDisciplineId(disciplineId, options));

        [HttpGet("{disciplineId:int}/Themes")]
        public IActionResult GetDisciplineThemes(int disciplineId, OptionsTheme options) =>
            Json(ManagerTheme.GetThemesByDisciplineId(disciplineId, options));
    }
}