using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Roles")]
    public class TamerRole : Tamer
    {
        protected IManagerUser ManagerUser { get; }
        protected IManagerRole ManagerRole { get; }

        public TamerRole(IManagerUser managerUser, IManagerRole managerRole)
        {
            ManagerUser = managerUser;
            ManagerRole = managerRole;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRoles(OptionsRole options) =>
            Json(ManagerRole.GetRoles(options));

        [HttpGet("{roleId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRole(int roleId, OptionsRole options) =>
            Json(ManagerRole.GetRoleById(roleId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetRole(OptionsRole options) =>
            Json(ManagerRole.GetRoleByUserId(GetUserId(), options));

        [HttpGet("{roleId:int}/Users")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRoleUsers(int roleId, OptionsUser options) =>
            Json(ManagerUser.GetUsersByRoleId(roleId, options));
    }
}