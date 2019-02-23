using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
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
        protected IManagerRole ManagerRole { get; }

        public TamerUser(IManagerUser managerUser, IManagerRole managerRole)
        {
            ManagerUser = managerUser;
            ManagerRole = managerRole;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUsers(
            [FromQuery] OptionsUser options,
            [FromQuery] Filter filter)
            => Ok(ManagerUser.GetUsers(options, filter));

        [HttpGet("{userId:int}")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUser(
            [FromRoute] int userId,
            [FromQuery] OptionsUser options)
            => Ok(ManagerUser.GetUserById(userId, options));

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetUser([FromQuery] OptionsUser options) =>
            Ok(ManagerUser.GetUserById(GetUserId(), options));

        [HttpGet("{userId:int}/Roles")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetUserRoles(
            [FromRoute] int userId,
            [FromQuery] OptionsRole options)
            => Ok(ManagerRole.GetRoleByUserId(userId, options));

        [Authorize]
        [HttpGet("Current/Roles")]
        public IActionResult GetUserRoles([FromQuery] OptionsRole options) =>
            Ok(ManagerRole.GetRoleByUserId(GetUserId(), options));
    }
}