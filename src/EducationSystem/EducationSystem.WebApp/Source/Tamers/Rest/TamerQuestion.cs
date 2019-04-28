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
    [Route("api/Questions")]
    public class TamerQuestion : Tamer
    {
        private readonly IManagerQuestion _managerQuestion;

        public TamerQuestion(IManagerQuestion managerQuestion)
        {
            _managerQuestion = managerQuestion;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetQuestions(
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
        {
            return Ok(await _managerQuestion.GetQuestions(options, filter));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
        {
            return Ok(await _managerQuestion.CreateQuestion(question));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetQuestion([FromRoute] int id, [FromQuery] OptionsQuestion options)
        {
            return Ok(await _managerQuestion.GetQuestion(id, options));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int id, [FromBody] Question question)
        {
            return Ok(await _managerQuestion.UpdateQuestion(id, question));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            await _managerQuestion.DeleteQuestion(id);

            return Ok();
        }
    }
}