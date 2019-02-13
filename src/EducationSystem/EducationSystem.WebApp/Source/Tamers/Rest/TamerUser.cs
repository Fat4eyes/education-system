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
        [Roles(UserRoles.Admin)]
        public IActionResult GetUsers(OptionsUser options) =>
            Json(ManagerUser.GetUsers(options));

        [HttpGet("{userId:int}")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUser(int userId, OptionsUser options) =>
            Json(ManagerUser.GetUserById(userId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetUser(OptionsUser options) =>
            Json(ManagerUser.GetUserById(GetUserId(), options));

        [HttpGet("{userId:int}/Roles")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUserRoles(int userId, OptionsRole options) =>
            Json(ManagerRole.GetRoleByUserId(userId, options));

        [Authorize]
        [HttpGet("Current/Roles")]
        public IActionResult GetUserRoles(OptionsRole options) =>
            Json(ManagerRole.GetRoleByUserId(GetUserId(), options));
    }
}