using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Students")]
    public class TamerStudent : Tamer
    {
        private readonly IManagerUser _managerUser;
        private readonly IManagerQuestion _managerQuestion;
        private readonly IManagerTestData _managerTestData;
        private readonly IManagerDiscipline _managerDiscipline;

        public TamerStudent(
            IManagerUser managerUser,
            IManagerQuestion managerQuestion,
            IManagerTestData managerTestData,
            IManagerDiscipline managerDiscipline)
        {
            _managerUser = managerUser;
            _managerQuestion = managerQuestion;
            _managerTestData = managerTestData;
            _managerDiscipline = managerDiscipline;
        }

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public async Task<IActionResult> GetStudent()
        {
            return Ok(await _managerUser.GetUserAsync(GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Tests/{testId:int}/Data")]
        public async Task<IActionResult> GetStudentTestData([FromRoute] int testId)
        {
            return Ok(await _managerTestData.GetTestDataForStudentByTestIdAsync(testId, GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Tests/{testId:int}/Questions")]
        public async Task<IActionResult> GetStudentTestQuestions([FromRoute] int testId)
        {
            return Ok(await _managerQuestion.GetQuestionsForStudentByTestIdAsync(testId, GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Disciplines")]
        public async Task<IActionResult> GetDisciplinesByStudentId(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
        {
            return Ok(await _managerDiscipline.GetDisciplinesByStudentIdAsync(GetUserId(), options, filter));
        }
    }
}