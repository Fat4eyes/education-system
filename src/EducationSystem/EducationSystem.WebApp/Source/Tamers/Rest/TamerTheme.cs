using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
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
        public async Task<IActionResult> CreateTheme([FromBody] Theme theme)
            => Ok(await _managerTheme.CreateThemeAsync(theme));

        [HttpGet("{themeId:int}")]
        public IActionResult GetTheme(
            [FromRoute] int themeId,
            [FromQuery] OptionsTheme options)
            => Ok(_managerTheme.GetThemeById(themeId, options));

        [Transaction]
        [HttpPut("{themeId:int}")]
        public async Task<IActionResult> UpdateTheme([FromRoute] int themeId, [FromBody] Theme theme)
            => Ok(await _managerTheme.UpdateThemeAsync(themeId, theme));

        [HttpGet("{themeId:int}/Questions")]
        public IActionResult GetThemeQuestions(
            [FromRoute] int themeId,
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
            => Ok(_managerQuestion.GetQuestionsByThemeId(themeId, options, filter));

        [Transaction]
        [HttpPut("{themeId:int}/Questions")]
        public IActionResult UpdateThemeQuestions(
            [FromRoute] int themeId,
            [FromBody] List<Question> questions)
            => Ok(async () => await _managerQuestion.UpdateThemeQuestionsAsync(themeId, questions));

        [Transaction]
        [HttpDelete("{themeId:int}")]
        public IActionResult DeleteTheme([FromRoute] int themeId) =>
            Ok(async () => await _managerTheme.DeleteThemeByIdAsync(themeId));
    }
}