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
    public class TamerTheme : Tamer
    {
        private readonly IManagerTheme _managerTheme;
        private readonly IManagerQuestion _managerQuestion;

        public TamerTheme(IManagerTheme managerTheme, IManagerQuestion managerQuestion)
        {
            _managerTheme = managerTheme;
            _managerQuestion = managerQuestion;
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTheme([FromRoute] int id, [FromQuery] OptionsTheme options)
        {
            return Ok(await _managerTheme.GetThemeAsync(id, options));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateTheme([FromBody] Theme theme)
        {
            return Ok(await _managerTheme.CreateThemeAsync(theme));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateTheme([FromRoute] int id, [FromBody] Theme theme)
        {
            return Ok(await _managerTheme.UpdateThemeAsync(id, theme));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteTheme([FromRoute] int id)
        {
            await _managerTheme.DeleteThemeAsync(id);

            return Ok();
        }

        [HttpGet("{id:int}/Questions")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetQuestionsByThemeId(
            [FromRoute] int id,
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
        {
            return Ok(await _managerQuestion.GetQuestionsByThemeIdAsync(id, options, filter));
        }

        [Transaction]
        [HttpPut("{id:int}/Questions")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateThemeQuestions([FromRoute] int id, [FromBody] List<Question> questions)
        {
            await _managerQuestion.UpdateThemeQuestionsAsync(id, questions);

            return Ok();
        }
    }
}