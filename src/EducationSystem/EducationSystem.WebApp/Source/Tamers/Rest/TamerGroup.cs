using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Groups")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerGroup : Tamer
    {
        protected IManagerStudent ManagerStudent { get; }
        protected IManagerGroup ManagerGroup { get; }

        public TamerGroup(IManagerStudent managerStudent, IManagerGroup managerGroup)
        {
            ManagerStudent = managerStudent;
            ManagerGroup = managerGroup;
        }

        [HttpGet("")]
        public IActionResult GetGroups(OptionsGroup options) =>
            Json(ManagerGroup.GetGroups(options));

        [HttpGet("{groupId:int}")]
        public IActionResult GetGroup(int groupId, OptionsGroup options) =>
            Json(ManagerGroup.GetGroupById(groupId, options));

        [HttpGet("{groupId:int}/Students")]
        public IActionResult GetGroupStudents(int groupId, OptionsStudent options) =>
            Json(ManagerStudent.GetStudentsByGroupId(groupId, options));
    }
}