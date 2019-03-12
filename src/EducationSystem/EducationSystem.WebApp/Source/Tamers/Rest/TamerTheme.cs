using System.Threading.Tasks;
using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Export;
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
        private readonly IManagerExportQuestion _managerExportQuestion;

        public TamerTheme(
            IManagerTheme managerTheme,
            IManagerQuestion managerQuestion,
            IManagerExportQuestion managerExportQuestion)
        {
            _managerTheme = managerTheme;
            _managerQuestion = managerQuestion;
            _managerExportQuestion = managerExportQuestion;
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
        [HttpDelete("{themeId:int}")]
        public IActionResult DeleteTheme([FromRoute] int themeId) =>
            Ok(async () => await _managerTheme.DeleteThemeByIdAsync(themeId));

        [HttpGet("{themeId:int}/Exports/Questions")]
        public IActionResult ExportThemeQuestions([FromRoute] int themeId) =>
            File(_managerExportQuestion.ExportThemeQuestions(themeId));
    }
}