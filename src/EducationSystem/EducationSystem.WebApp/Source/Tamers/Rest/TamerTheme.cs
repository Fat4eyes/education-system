using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Themes")]
    public class TamerTheme : Tamer
    {
        private readonly IServiceTheme _serviceTheme;
        private readonly IServiceQuestion _serviceQuestion;

        public TamerTheme(IServiceTheme serviceTheme, IServiceQuestion serviceQuestion)
        {
            _serviceTheme = serviceTheme;
            _serviceQuestion = serviceQuestion;
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetThemes([FromQuery] FilterTheme filter)
        {
            return await Ok(() => _serviceTheme.GetThemesAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTheme([FromRoute] int id)
        {
            return await Ok(() => _serviceTheme.GetThemeAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> CreateTheme([FromBody] Theme theme)
        {
            return await Ok(() => _serviceTheme.CreateThemeAsync(theme));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateTheme([FromRoute] int id, [FromBody] Theme theme)
        {
            return await Ok(() => _serviceTheme.UpdateThemeAsync(id, theme));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteTheme([FromRoute] int id)
        {
            return await Ok(() => _serviceTheme.DeleteThemeAsync(id));
        }

        [HttpGet("{id:int}/Questions")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetThemeQuestions([FromRoute] int id, [FromQuery] FilterQuestion filter)
        {
            return await Ok(() => _serviceQuestion.GetQuestionsAsync(filter.SetThemeId(id)));
        }

        [HttpPut("{id:int}/Questions")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateThemeQuestions([FromRoute] int id, [FromBody] List<Question> questions)
        {
            return await Ok(() => _serviceQuestion.UpdateThemeQuestionsAsync(id, questions));
        }
    }
}