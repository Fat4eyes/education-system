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
        private readonly IManagerTheme _managerTheme;
        private readonly IManagerQuestion _managerQuestion;

        public TamerTheme(IManagerTheme managerTheme, IManagerQuestion managerQuestion)
        {
            _managerTheme = managerTheme;
            _managerQuestion = managerQuestion;
        }

        [HttpGet("")]
        public IActionResult GetThemes(
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(_managerTheme.GetThemes(options, filter));

        [HttpGet("{themeId:int}")]
        public IActionResult GetTheme(
            [FromRoute] int themeId,
            [FromQuery] OptionsTheme options)
            => Ok(_managerTheme.GetThemeById(themeId, options));

        [HttpGet("{themeId:int}/Questions")]
        public IActionResult GetThemeQuestions(
            [FromRoute] int themeId,
            [FromQuery] OptionsQuestion options,
            [FromQuery] Filter filter)
            => Ok(_managerQuestion.GetQuestionsByThemeId(themeId, options, filter));

        [HttpDelete("{themeId:int}")]
        public IActionResult DeleteTheme([FromRoute] int themeId) =>
            Ok(() => _managerTheme.DeleteThemeById(themeId));
    }
}