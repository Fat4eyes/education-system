using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
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
        public IActionResult GetRoles(
            [FromQuery] OptionsRole options,
            [FromQuery] Filter filter)
            => Ok(ManagerRole.GetRoles(options, filter));

        [HttpGet("{roleId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRole(
            [FromRoute] int roleId,
            [FromQuery] OptionsRole options)
            => Ok(ManagerRole.GetRoleById(roleId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetRole([FromQuery] OptionsRole options)
            => Ok(ManagerRole.GetRoleByUserId(GetUserId(), options));

        [HttpGet("{roleId:int}/Users")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRoleUsers(
            [FromRoute] int roleId,
            [FromQuery] OptionsUser options,
            [FromQuery] Filter filter)
            => Ok(ManagerUser.GetUsersByRoleId(roleId, options, filter));
    }
}