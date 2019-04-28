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
        private readonly IManagerStudent _managerStudent;
        private readonly IManagerQuestion _managerQuestion;
        private readonly IManagerTestData _managerTestData;
        private readonly IManagerStudyPlan _managerStudyPlan;
        private readonly IManagerInstitute _managerInstitute;
        private readonly IManagerDiscipline _managerDiscipline;
        private readonly IManagerStudyProfile _managerStudyProfile;

        public TamerStudent(
            IManagerStudent managerStudent,
            IManagerQuestion managerQuestion,
            IManagerTestData managerTestData,
            IManagerStudyPlan managerStudyPlan,
            IManagerInstitute managerInstitute,
            IManagerDiscipline managerDiscipline,
            IManagerStudyProfile managerStudyProfile)
        {
            _managerStudent = managerStudent;
            _managerQuestion = managerQuestion;
            _managerTestData = managerTestData;
            _managerStudyPlan = managerStudyPlan;
            _managerInstitute = managerInstitute;
            _managerDiscipline = managerDiscipline;
            _managerStudyProfile = managerStudyProfile;
        }

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public async Task<IActionResult> GetStudent()
        {
            return Ok(await _managerStudent.GetStudent(GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/StudyPlan")]
        public async Task<IActionResult> GetStudyPlanByStudentId([FromQuery] OptionsStudyPlan options)
        {
            return Ok(await _managerStudyPlan.GetStudyPlanByStudentId(GetUserId(), options));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/StudyProfile")]
        public async Task<IActionResult> GetStudentStudyProfile([FromQuery] OptionsStudyProfile options)
        {
            return Ok(await _managerStudyProfile.GetStudyProfileByStudentId(GetUserId(), options));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Institute")]
        public async Task<IActionResult> GetStudentInstitute([FromQuery] OptionsInstitute options)
        {
            return Ok(await _managerInstitute.GetInstituteByStudentId(GetUserId(), options));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Tests/{testId:int}/Data")]
        public async Task<IActionResult> GetStudentTestData([FromRoute] int testId)
        {
            return Ok(await _managerTestData.GetTestDataForStudentByTestId(testId, GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Tests/{testId:int}/Questions")]
        public async Task<IActionResult> GetStudentTestQuestions([FromRoute] int testId)
        {
            return Ok(await _managerQuestion.GetQuestionsForStudentByTestId(testId, GetUserId()));
        }

        [Roles(UserRoles.Student)]
        [HttpGet("Current/Disciplines")]
        public async Task<IActionResult> GetDisciplinesByStudentId(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
        {
            return Ok(await _managerDiscipline.GetDisciplinesByStudentId(GetUserId(), options, filter));
        }
    }
}