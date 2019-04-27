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
        private readonly IManagerTest _managerTest;
        private readonly IManagerGroup _managerGroup;
        private readonly IManagerStudent _managerStudent;
        private readonly IManagerQuestion _managerQuestion;
        private readonly IManagerTestData _managerTestData;
        private readonly IManagerStudyPlan _managerStudyPlan;
        private readonly IManagerInstitute _managerInstitute;
        private readonly IManagerDiscipline _managerDiscipline;
        private readonly IManagerStudyProfile _managerStudyProfile;

        public TamerStudent(
            IManagerTest managerTest,
            IManagerGroup managerGroup,
            IManagerStudent managerStudent,
            IManagerQuestion managerQuestion,
            IManagerTestData managerTestData,
            IManagerStudyPlan managerStudyPlan,
            IManagerInstitute managerInstitute,
            IManagerDiscipline managerDiscipline,
            IManagerStudyProfile managerStudyProfile)
        {
            _managerTest = managerTest;
            _managerGroup = managerGroup;
            _managerStudent = managerStudent;
            _managerQuestion = managerQuestion;
            _managerTestData = managerTestData;
            _managerStudyPlan = managerStudyPlan;
            _managerInstitute = managerInstitute;
            _managerDiscipline = managerDiscipline;
            _managerStudyProfile = managerStudyProfile;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult GetStudents(
            [FromQuery] OptionsStudent options,
            [FromQuery] FilterStudent filter)
            => Ok(_managerStudent.GetStudents(options, filter));

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudent([FromQuery] OptionsStudent options)
            => Ok(_managerStudent.GetStudentById(GetUserId(), options));

        [HttpGet("Current/Group")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentGroup([FromQuery] OptionsGroup options)
            => Ok(_managerGroup.GetGroupByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Group")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult GetStudentGroup(
            [FromRoute] int studentId,
            [FromQuery] OptionsGroup options)
            => Ok(_managerGroup.GetGroupByStudentId(studentId, options));

        [HttpGet("Current/StudyPlan")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyPlan([FromQuery] OptionsStudyPlan options)
            => Ok(_managerStudyPlan.GetStudyPlanByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyPlan")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult GetStudentStudyPlan(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyPlan options)
            => Ok(_managerStudyPlan.GetStudyPlanByStudentId(studentId, options));

        [HttpGet("Current/StudyProfile")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyProfile([FromQuery] OptionsStudyProfile options)
            => Ok(_managerStudyProfile.GetStudyProfileByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyProfile")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult GetUserStudyProfile(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyProfile options)
            => Ok(_managerStudyProfile.GetStudyProfileByStudentId(studentId, options));

        [HttpGet("Current/Institute")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentInstitute([FromQuery] OptionsInstitute options)
            => Ok(_managerInstitute.GetInstituteByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Institute")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult GetUserInstitute(
            [FromRoute] int studentId,
            [FromQuery] OptionsInstitute options)
            => Ok(_managerInstitute.GetInstituteByStudentId(studentId, options));

        [HttpGet("Current/Tests")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTests(
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(_managerTest.GetTestsForStudent(GetUserId(), options, filter));

        [HttpGet("Current/Tests/{testId:int}")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTest(
            [FromRoute] int testId,
            [FromQuery] OptionsTest options)
            => Ok(_managerTest.GetTestForStudentById(testId, GetUserId(), options));

        [HttpGet("Current/Tests/{testId:int}/Data")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestData([FromRoute] int testId)
            => Ok(_managerTestData.GetTestDataForStudentByTestId(testId, GetUserId()));

        [HttpGet("Current/Tests/Data")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestsData([FromQuery] int[] testIds)
            => Ok(_managerTestData.GetTestsDataForStudentByTestIds(testIds, GetUserId()));

        [HttpGet("Current/Tests/{testId:int}/Questions")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestQuestions([FromRoute] int testId)
            => Ok(_managerQuestion.GetQuestionsForStudentByTestId(testId, GetUserId()));

        [HttpGet("Current/Disciplines")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
            => Ok(_managerDiscipline.GetDisciplinesForStudent(GetUserId(), options, filter));
    }
}