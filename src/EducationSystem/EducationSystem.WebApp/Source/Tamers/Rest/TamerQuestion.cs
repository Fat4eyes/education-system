using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Questions")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerQuestion : Tamer
    {
        private readonly IManagerQuestion _managerQuestion;

        public TamerQuestion(IManagerQuestion managerQuestion)
        {
            _managerQuestion = managerQuestion;
        }

        [HttpGet("")]
        public IActionResult GetQuestions(
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
            => Ok(_managerQuestion.GetQuestions(options, filter));

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
            => Ok(await _managerQuestion.CreateQuestionAsync(question));

        [HttpGet("{questionId:int}")]
        public IActionResult GetQuestion(
            [FromRoute] int questionId,
            [FromQuery] OptionsQuestion options)
            => Ok(_managerQuestion.GetQuestionById(questionId, options));

        [Transaction]
        [HttpPut("{questionId:int}")]
        public async Task<IActionResult> UpdateQuestion(
            [FromRoute] int questionId,
            [FromBody] Question question)
            => Ok(await _managerQuestion.UpdateQuestionAsync(questionId, question));

        [Transaction]
        [HttpDelete("{questionId:int}")]
        public IActionResult DeleteQuestion([FromRoute] int questionId) =>
            Ok(async () => await _managerQuestion.DeleteQuestionByIdAsync(questionId));
    }
}