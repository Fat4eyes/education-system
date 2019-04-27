using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Groups")]
    [Roles(UserRoles.Admin, UserRoles.Lecturer)]
    public class TamerGroup : Tamer
    {
        private readonly IManagerGroup _managerGroup;
        private readonly IManagerStudent _managerStudent;

        public TamerGroup(IManagerGroup managerGroup, IManagerStudent managerStudent)
        {
            _managerGroup = managerGroup;
            _managerStudent = managerStudent;
        }

        [HttpGet("")]
        public IActionResult GetGroups(
            [FromQuery] OptionsGroup options,
            [FromQuery] FilterGroup filter)
            => Ok(_managerGroup.GetGroups(options, filter));

        [HttpGet("{groupId:int}")]
        public IActionResult GetGroup(
            [FromRoute] int groupId,
            [FromQuery] OptionsGroup options)
            => Ok(_managerGroup.GetGroupById(groupId, options));

        [HttpGet("{groupId:int}/Students")]
        public IActionResult GetGroupStudents(
            [FromRoute] int groupId,
            [FromQuery] OptionsStudent options,
            [FromQuery] FilterStudent filter)
            => Ok(_managerStudent.GetStudentsByGroupId(groupId, options, filter));
    }
}