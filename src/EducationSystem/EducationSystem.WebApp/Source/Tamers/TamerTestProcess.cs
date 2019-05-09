using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Roles(UserRoles.Student)]
    [Route("api/Tests/{id:int}/Process")]
    public class TamerTestProcess : Tamer
    {
        private readonly IServiceTestProcess _serviceTestProcess;

        public TamerTestProcess(IServiceTestProcess serviceTestProcess)
        {
            _serviceTestProcess = serviceTestProcess;
        }

        [HttpGet]
        [Route("Question")]
        public async Task<IActionResult> GetQuestion([FromRoute] int id)
        {
            return await Ok(() => _serviceTestProcess.GetQuestionAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Route("Question")]
        public async Task<IActionResult> ProcessQuestion([FromRoute] int id, [FromBody] Question question)
        {
            return await Ok(() => _serviceTestProcess.ProcessQuestionAsync(id, question.SetTestId(id)));
        }
    }
}