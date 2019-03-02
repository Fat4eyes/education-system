using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
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

        [Transaction]
        [HttpPost("")]
        public IActionResult CreateTheme([FromBody] Theme theme)
            => Ok(_managerTheme.CreateTheme(theme));

        [HttpGet("{themeId:int}")]
        public IActionResult GetTheme(
            [FromRoute] int themeId,
            [FromQuery] OptionsTheme options)
            => Ok(_managerTheme.GetThemeById(themeId, options));

        [Transaction]
        [HttpPut("{themeId:int}")]
        public IActionResult UpdateTheme([FromRoute] int themeId, [FromBody] Theme theme)
            => Ok(_managerTheme.UpdateTheme(themeId, theme));

        [HttpGet("{themeId:int}/Questions")]
        public IActionResult GetThemeQuestions(
            [FromRoute] int themeId,
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
            => Ok(_managerQuestion.GetQuestionsByThemeId(themeId, options, filter));

        [Transaction]
        [HttpDelete("{themeId:int}")]
        public IActionResult DeleteTheme([FromRoute] int themeId) =>
            Ok(() => _managerTheme.DeleteThemeById(themeId));
    }
}