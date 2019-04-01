using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Roles")]
    public class TamerRole : Tamer
    {
        private readonly IManagerUser _managerUser;
        private readonly IManagerRole _managerRole;

        public TamerRole(IManagerUser managerUser, IManagerRole managerRole)
        {
            _managerUser = managerUser;
            _managerRole = managerRole;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRoles(
            [FromQuery] OptionsRole options,
            [FromQuery] FilterRole filter)
            => Ok(_managerRole.GetRoles(options, filter));

        [HttpGet("{roleId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRole(
            [FromRoute] int roleId,
            [FromQuery] OptionsRole options)
            => Ok(_managerRole.GetRoleById(roleId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetRole([FromQuery] OptionsRole options)
            => Ok(_managerRole.GetRoleByUserId(GetUserId(), options));

        [HttpGet("{roleId:int}/Users")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetRoleUsers(
            [FromRoute] int roleId,
            [FromQuery] OptionsUser options,
            [FromQuery] FilterUser filter)
            => Ok(_managerUser.GetUsersByRoleId(roleId, options, filter));
    }
}