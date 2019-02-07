using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Themes")]
    public class TamerTheme : Tamer
    {
        protected IManagerTheme ManagerTheme { get; }
        protected IManagerQuestion ManagerQuestion { get; }

        public TamerTheme(IManagerTheme managerTheme, IManagerQuestion managerQuestion)
        {
            ManagerTheme = managerTheme;
            ManagerQuestion = managerQuestion;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetThemes(OptionsTheme options)
        {
            return Json(ManagerTheme.GetThemes(options));
        }

        [HttpGet("{themeId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTheme(int themeId, OptionsTheme options)
        {
            return Json(ManagerTheme.GetThemeById(themeId, options));
        }

        [HttpGet("{themeId:int}/Questions")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetThemeQuestions(int themeId, OptionsQuestion options)
        {
            return Json(ManagerQuestion.GetQuestionsByThemeId(themeId, options));
        }
    }
}