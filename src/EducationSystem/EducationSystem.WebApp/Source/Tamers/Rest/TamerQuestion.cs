using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Questions")]
    public class TamerQuestion : Tamer
    {
        private readonly IServiceQuestion _serviceQuestion;

        public TamerQuestion(IServiceQuestion serviceQuestion)
        {
            _serviceQuestion = serviceQuestion;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetQuestions([FromQuery] FilterQuestion filter)
        {
            return await Ok(() => _serviceQuestion.GetQuestionsAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetQuestion([FromRoute] int id)
        {
            return await Ok(() => _serviceQuestion.GetQuestionAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
        {
            return await Ok(() => _serviceQuestion.CreateQuestionAsync(question));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int id, [FromBody] Question question)
        {
            return await Ok(() => _serviceQuestion.UpdateQuestionAsync(id, question));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            return await Ok(() => _serviceQuestion.DeleteQuestionAsync(id));
        }
    }
}