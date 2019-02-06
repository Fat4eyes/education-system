using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Users")]
    public class TamerUser : Tamer
    {
        protected IManagerUser ManagerUser { get; }
        protected IManagerGroup ManagerGroup { get; }
        protected IManagerStudyPlan ManagerStudyPlan { get; }
        protected IManagerStudyProfile ManagerStudyProfile { get; }
        protected IManagerInstitute ManagerInstitute { get; }
        protected IManagerRole ManagerRole { get; }
        protected IManagerTestResult ManagerTestResult { get; }

        public TamerUser(
            IManagerUser managerUser,
            IManagerGroup managerGroup,
            IManagerStudyPlan managerStudyPlan,
            IManagerStudyProfile managerStudyProfile,
            IManagerInstitute managerInstitute,
            IManagerRole managerRole,
            IManagerTestResult managerTestResult)
        {
            ManagerUser = managerUser;
            ManagerGroup = managerGroup;
            ManagerStudyPlan = managerStudyPlan;
            ManagerStudyProfile = managerStudyProfile;
            ManagerInstitute = managerInstitute;
            ManagerRole = managerRole;
            ManagerTestResult = managerTestResult;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUsers(OptionsUser options)
        {
            return Json(ManagerUser.GetUsers(options));
        }

        [HttpGet("{userId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUser(int userId, OptionsUser options)
        {
            return Json(ManagerUser.GetUserById(userId, options));
        }

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetUser(OptionsUser options)
        {
            return Json(ManagerUser.GetUserById(GetUserId(), options));
        }

        [HttpGet("{userId:int}/Group")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserGroup(int userId, OptionsGroup options)
        {
            return Json(ManagerGroup.GetGroupByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/Group")]
        public IActionResult GetUserGroup(OptionsGroup options)
        {
            return Json(ManagerGroup.GetGroupByUserId(GetUserId(), options));
        }

        [HttpGet("{userId:int}/StudyPlan")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserStudyPlan(int userId, OptionsStudyPlan options)
        {
            return Json(ManagerStudyPlan.GetStudyPlanByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/StudyPlan")]
        public IActionResult GetUserStudyPlan(OptionsStudyPlan options)
        {
            return Json(ManagerStudyPlan.GetStudyPlanByUserId(GetUserId(), options));
        }

        [HttpGet("{userId:int}/StudyProfile")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserStudyProfile(int userId, OptionsStudyProfile options)
        {
            return Json(ManagerStudyProfile.GetStudyProfileByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/StudyProfile")]
        public IActionResult GetUserStudyProfile(OptionsStudyProfile options)
        {
            return Json(ManagerStudyProfile.GetStudyProfileByUserId(GetUserId(), options));
        }

        [HttpGet("{userId:int}/Institute")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserInstitute(int userId, OptionsInstitute options)
        {
            return Json(ManagerInstitute.GetInstituteByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/Institute")]
        public IActionResult GetUserInstitute(OptionsInstitute options)
        {
            return Json(ManagerInstitute.GetInstituteByUserId(GetUserId(), options));
        }

        [HttpGet("{userId:int}/Roles")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserRoles(int userId, OptionsRole options)
        {
            return Json(ManagerRole.GetRoleByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/Roles")]
        public IActionResult GetUserRoles(OptionsRole options)
        {
            return Json(ManagerRole.GetRoleByUserId(GetUserId(), options));
        }

        [HttpGet("{userId:int}/TestResults")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserTestResults(int userId, OptionsTestResult options)
        {
            return Json(ManagerTestResult.GetTestResultsByUserId(userId, options));
        }

        [Authorize]
        [HttpGet("Current/TestResults")]
        public IActionResult GetUserTestResults(OptionsTestResult options)
        {
            return Json(ManagerTestResult.GetTestResultsByUserId(GetUserId(), options));
        }
    }
}