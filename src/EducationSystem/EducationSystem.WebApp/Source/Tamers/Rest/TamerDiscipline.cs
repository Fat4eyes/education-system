using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
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
        public IActionResult GetDisciplines(OptionsDiscipline options, Filter filter) =>
            Json(ManagerDiscipline.GetDisciplines(options, filter));

        [HttpGet("{disciplineId:int}")]
        public IActionResult GetDiscipline(int disciplineId, OptionsDiscipline options) =>
            Json(ManagerDiscipline.GetDisciplineById(disciplineId, options));

        [HttpGet("{disciplineId:int}/Tests")]
        public IActionResult GetDisciplineTests(int disciplineId, OptionsTest options, FilterTest filter) =>
            Json(ManagerTest.GetTestsByDisciplineId(disciplineId, options, filter));

        [HttpGet("{disciplineId:int}/Themes")]
        public IActionResult GetDisciplineThemes(int disciplineId, OptionsTheme options, FilterTheme filter) =>
            Json(ManagerTheme.GetThemesByDisciplineId(disciplineId, options, filter));
    }
}