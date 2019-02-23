using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Students")]
    public class TamerStudent : Tamer
    {
        protected IManagerStudent ManagerStudent { get; }
        protected IManagerGroup ManagerGroup { get; }
        protected IManagerStudyPlan ManagerStudyPlan { get; }
        protected IManagerStudyProfile ManagerStudyProfile { get; }
        protected IManagerInstitute ManagerInstitute { get; }
        protected IManagerTestResult ManagerTestResult { get; }
        protected IManagerDiscipline ManagerDiscipline { get; }

        public TamerStudent(
            IManagerStudent managerStudent,
            IManagerGroup managerGroup,
            IManagerStudyPlan managerStudyPlan,
            IManagerStudyProfile managerStudyProfile,
            IManagerInstitute managerInstitute,
            IManagerTestResult managerTestResult,
            IManagerDiscipline managerDiscipline)
        {
            ManagerStudent = managerStudent;
            ManagerGroup = managerGroup;
            ManagerStudyPlan = managerStudyPlan;
            ManagerStudyProfile = managerStudyProfile;
            ManagerInstitute = managerInstitute;
            ManagerTestResult = managerTestResult;
            ManagerDiscipline = managerDiscipline;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudents(
            [FromQuery] OptionsStudent options,
            [FromQuery] Filter filter)
            => Ok(ManagerStudent.GetStudents(options, filter));

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudent([FromQuery] OptionsStudent options)
            => Ok(ManagerStudent.GetStudentById(GetUserId(), options));

        [HttpGet("Current/Group")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentGroup([FromQuery] OptionsGroup options)
            => Ok(ManagerGroup.GetGroupByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Group")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentGroup(
            [FromRoute] int studentId,
            [FromQuery] OptionsGroup options)
            => Ok(ManagerGroup.GetGroupByStudentId(studentId, options));

        [HttpGet("Current/StudyPlan")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyPlan([FromQuery] OptionsStudyPlan options)
            => Ok(ManagerStudyPlan.GetStudyPlanByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyPlan")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentStudyPlan(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyPlan options)
            => Ok(ManagerStudyPlan.GetStudyPlanByStudentId(studentId, options));

        [HttpGet("Current/StudyProfile")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyProfile([FromQuery] OptionsStudyProfile options)
            => Ok(ManagerStudyProfile.GetStudyProfileByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyProfile")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserStudyProfile(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyProfile options)
            => Ok(ManagerStudyProfile.GetStudyProfileByStudentId(studentId, options));

        [HttpGet("Current/Institute")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentInstitute([FromQuery] OptionsInstitute options)
            => Ok(ManagerInstitute.GetInstituteByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Institute")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserInstitute(
            [FromRoute] int studentId,
            [FromQuery] OptionsInstitute options)
            => Ok(ManagerInstitute.GetInstituteByStudentId(studentId, options));

        [HttpGet("Current/TestResults")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestResults(
            [FromQuery] OptionsTestResult options,
            [FromQuery] Filter filter)
            => Ok(ManagerTestResult.GetTestResultsByStudentId(GetUserId(), options, filter));

        [HttpGet("{studentId:int}/TestResults")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserTestResults(
            [FromRoute] int studentId,
            [FromQuery] OptionsTestResult options,
            [FromQuery] Filter filter)
            => Ok(ManagerTestResult.GetTestResultsByStudentId(studentId, options, filter));

        [HttpGet("Current/Disciplines")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] Filter filter)
            => Ok(ManagerDiscipline.GetDisciplinesByStudentId(GetUserId(), options, filter));
    }
}