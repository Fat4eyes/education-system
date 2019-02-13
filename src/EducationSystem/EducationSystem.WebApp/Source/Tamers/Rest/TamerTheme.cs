using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Themes")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
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
        public IActionResult GetThemes(OptionsTheme options) =>
            Json(ManagerTheme.GetThemes(options));

        [HttpGet("{themeId:int}")]
        public IActionResult GetTheme(int themeId, OptionsTheme options) =>
            Json(ManagerTheme.GetThemeById(themeId, options));

        [HttpGet("{themeId:int}/Questions")]
        public IActionResult GetThemeQuestions(int themeId, OptionsQuestion options) =>
            Json(ManagerQuestion.GetQuestionsByThemeId(themeId, options));
    }
}