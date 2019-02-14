using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
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
        public IActionResult GetStudents(OptionsStudent options) =>
            Json(ManagerStudent.GetStudents(options));

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudent(OptionsStudent options) =>
            Json(ManagerStudent.GetStudentById(GetUserId(), options));

        [HttpGet("Current/Group")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentGroup(OptionsGroup options) =>
            Json(ManagerGroup.GetGroupByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Group")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentGroup(int studentId, OptionsGroup options) =>
            Json(ManagerGroup.GetGroupByStudentId(studentId, options));

        [HttpGet("Current/StudyPlan")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyPlan(OptionsStudyPlan options) =>
            Json(ManagerStudyPlan.GetStudyPlanByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyPlan")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentStudyPlan(int studentId, OptionsStudyPlan options) =>
            Json(ManagerStudyPlan.GetStudyPlanByStudentId(studentId, options));

        [HttpGet("Current/StudyProfile")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyProfile(OptionsStudyProfile options) =>
            Json(ManagerStudyProfile.GetStudyProfileByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyProfile")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserStudyProfile(int studentId, OptionsStudyProfile options) =>
            Json(ManagerStudyProfile.GetStudyProfileByStudentId(studentId, options));

        [HttpGet("Current/Institute")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentInstitute(OptionsInstitute options) =>
            Json(ManagerInstitute.GetInstituteByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Institute")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserInstitute(int studentId, OptionsInstitute options) =>
            Json(ManagerInstitute.GetInstituteByStudentId(studentId, options));

        [HttpGet("Current/TestResults")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestResults(OptionsTestResult options) =>
            Json(ManagerTestResult.GetTestResultsByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/TestResults")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserTestResults(int studentId, OptionsTestResult options) =>
            Json(ManagerTestResult.GetTestResultsByStudentId(studentId, options));

        [HttpGet("Current/Disciplines")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentDisciplines(OptionsDiscipline options) =>
            Json(ManagerDiscipline.GetDisciplinesByStudentId(GetUserId(), options));
    }
}