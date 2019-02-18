using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
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
        public IActionResult GetGroups(OptionsGroup options, FilterGroup filter) =>
            Json(ManagerGroup.GetGroups(options, filter));

        [HttpGet("{groupId:int}")]
        public IActionResult GetGroup(int groupId, OptionsGroup options) =>
            Json(ManagerGroup.GetGroupById(groupId, options));

        [HttpGet("{groupId:int}/Students")]
        public IActionResult GetGroupStudents(int groupId, OptionsStudent options, Filter filter) =>
            Json(ManagerStudent.GetStudentsByGroupId(groupId, options, filter));
    }
}