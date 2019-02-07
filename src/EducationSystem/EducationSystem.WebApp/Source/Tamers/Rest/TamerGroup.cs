using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Groups")]
    public class TamerGroup : Tamer
    {
        protected IManagerUser ManagerUser { get; }
        protected IManagerGroup ManagerGroup { get; }

        public TamerGroup(IManagerUser managerUser, IManagerGroup managerGroup)
        {
            ManagerUser = managerUser;
            ManagerGroup = managerGroup;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetGroups(OptionsGroup options)
        {
            return Json(ManagerGroup.GetGroups(options));
        }

        [HttpGet("{groupId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetGroup(int groupId, OptionsGroup options)
        {
            return Json(ManagerGroup.GetGroupById(groupId, options));
        }

        [Authorize]
        [HttpGet("Current")]
        public IActionResult GetGroup(OptionsGroup options)
        {
            return Json(ManagerGroup.GetGroupByUserId(GetUserId(), options));
        }

        [HttpGet("{groupId:int}/Users")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetGroupUsers(int groupId, OptionsUser options)
        {
            return Json(ManagerUser.GetUsersByGroupId(groupId, options));
        }
    }
}