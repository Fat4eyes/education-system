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
        public IActionResult GetGroups(
            [FromQuery] OptionsGroup options,
            [FromQuery] FilterGroup filter)
            => Ok(ManagerGroup.GetGroups(options, filter));

        [HttpGet("{groupId:int}")]
        public IActionResult GetGroup(
            [FromRoute] int groupId,
            [FromQuery] OptionsGroup options)
            => Ok(ManagerGroup.GetGroupById(groupId, options));

        [HttpGet("{groupId:int}/Students")]
        public IActionResult GetGroupStudents(
            [FromRoute] int groupId,
            [FromQuery] OptionsStudent options,
            [FromQuery] Filter filter)
            => Ok(ManagerStudent.GetStudentsByGroupId(groupId, options, filter));
    }
}