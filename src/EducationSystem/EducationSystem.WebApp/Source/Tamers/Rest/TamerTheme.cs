using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
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
        public IActionResult GetThemes(
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(ManagerTheme.GetThemes(options, filter));

        [HttpGet("{themeId:int}")]
        public IActionResult GetTheme(
            [FromRoute] int themeId,
            [FromQuery] OptionsTheme options)
            => Ok(ManagerTheme.GetThemeById(themeId, options));

        [HttpGet("{themeId:int}/Questions")]
        public IActionResult GetThemeQuestions(
            [FromRoute] int themeId,
            [FromQuery] OptionsQuestion options,
            [FromQuery] Filter filter)
            => Ok(ManagerQuestion.GetQuestionsByThemeId(themeId, options, filter));

        [HttpDelete("{themeId:int}")]
        public IActionResult DeleteTheme([FromRoute] int themeId) =>
            Ok(() => ManagerTheme.DeleteThemeById(themeId));
    }
}