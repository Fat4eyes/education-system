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
        public IActionResult GetUsers(OptionsUser options, Filter filter) =>
            Json(ManagerUser.GetUsers(options, filter));

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