using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Users")]
    public class TamerUser : Tamer
    {
        private readonly IManagerUser _managerUser;
        private readonly IManagerRole _managerRole;

        public TamerUser(IManagerUser managerUser, IManagerRole managerRole)
        {
            _managerUser = managerUser;
            _managerRole = managerRole;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUsers(
            [FromQuery] OptionsUser options,
            [FromQuery] FilterUser filter)
            => Ok(_managerUser.GetUsers(options, filter));

        [HttpGet("{userId:int}")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUser(
            [FromRoute] int userId,
            [FromQuery] OptionsUser options)
            => Ok(_managerUser.GetUserById(userId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetUser([FromQuery] OptionsUser options) =>
            Ok(_managerUser.GetUserById(GetUserId(), options));

        [HttpGet("{userId:int}/Roles")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUserRoles(
            [FromRoute] int userId,
            [FromQuery] OptionsRole options)
            => Ok(_managerRole.GetRoleByUserId(userId, options));

        [Authorize]
        [HttpGet("Current/Roles")]
        public IActionResult GetUserRoles([FromQuery] OptionsRole options) =>
            Ok(_managerRole.GetRoleByUserId(GetUserId(), options));
    }
}