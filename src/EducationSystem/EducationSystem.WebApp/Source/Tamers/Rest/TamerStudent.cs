﻿using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Students")]
    public class TamerStudent : Tamer
    {
        private readonly IManagerTest _managerTest;
        private readonly IManagerGroup _managerGroup;
        private readonly IManagerStudent _managerStudent;
        private readonly IManagerTestData _managerTestData;
        private readonly IManagerStudyPlan _managerStudyPlan;
        private readonly IManagerInstitute _managerInstitute;
        private readonly IManagerTestResult _managerTestResult;
        private readonly IManagerDiscipline _managerDiscipline;
        private readonly IManagerStudyProfile _managerStudyProfile;
        private readonly IManagerTestExecution _managerTestExecution;

        public TamerStudent(
            IManagerTest managerTest,
            IManagerGroup managerGroup,
            IManagerStudent managerStudent,
            IManagerTestData managerTestData,
            IManagerQuestion managerQuestion,
            IManagerStudyPlan managerStudyPlan,
            IManagerInstitute managerInstitute,
            IManagerTestResult managerTestResult,
            IManagerDiscipline managerDiscipline,
            IManagerStudyProfile managerStudyProfile,
            IManagerTestExecution managerTestExecution)
        {
            _managerTest = managerTest;
            _managerGroup = managerGroup;
            _managerStudent = managerStudent;
            _managerTestData = managerTestData;
            _managerStudyPlan = managerStudyPlan;
            _managerInstitute = managerInstitute;
            _managerTestResult = managerTestResult;
            _managerDiscipline = managerDiscipline;
            _managerStudyProfile = managerStudyProfile;
            _managerTestExecution = managerTestExecution;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudents(
            [FromQuery] OptionsStudent options,
            [FromQuery] FilterStudent filter)
            => Ok(_managerStudent.GetStudents(options, filter));

        [HttpGet("Current")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudent([FromQuery] OptionsStudent options)
            => Ok(_managerStudent.GetStudentById(GetUserId(), options));

        [HttpGet("Current/Group")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentGroup([FromQuery] OptionsGroup options)
            => Ok(_managerGroup.GetGroupByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Group")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentGroup(
            [FromRoute] int studentId,
            [FromQuery] OptionsGroup options)
            => Ok(_managerGroup.GetGroupByStudentId(studentId, options));

        [HttpGet("Current/StudyPlan")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyPlan([FromQuery] OptionsStudyPlan options)
            => Ok(_managerStudyPlan.GetStudyPlanByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyPlan")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetStudentStudyPlan(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyPlan options)
            => Ok(_managerStudyPlan.GetStudyPlanByStudentId(studentId, options));

        [HttpGet("Current/StudyProfile")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentStudyProfile([FromQuery] OptionsStudyProfile options)
            => Ok(_managerStudyProfile.GetStudyProfileByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/StudyProfile")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserStudyProfile(
            [FromRoute] int studentId,
            [FromQuery] OptionsStudyProfile options)
            => Ok(_managerStudyProfile.GetStudyProfileByStudentId(studentId, options));

        [HttpGet("Current/Institute")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentInstitute([FromQuery] OptionsInstitute options)
            => Ok(_managerInstitute.GetInstituteByStudentId(GetUserId(), options));

        [HttpGet("{studentId:int}/Institute")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserInstitute(
            [FromRoute] int studentId,
            [FromQuery] OptionsInstitute options)
            => Ok(_managerInstitute.GetInstituteByStudentId(studentId, options));

        [HttpGet("Current/TestResults")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestResults(
            [FromQuery] OptionsTestResult options,
            [FromQuery] FilterTestResult filter)
            => Ok(_managerTestResult.GetTestResultsByStudentId(GetUserId(), options, filter));

        [HttpGet("{studentId:int}/TestResults")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetUserTestResults(
            [FromRoute] int studentId,
            [FromQuery] OptionsTestResult options,
            [FromQuery] FilterTestResult filter)
            => Ok(_managerTestResult.GetTestResultsByStudentId(studentId, options, filter));

        [HttpGet("Current/Tests")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTests(
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(_managerTest.GetTestsForStudent(GetUserId(), options, filter));

        [HttpGet("Current/Tests/{testId:int}")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTest(
            [FromRoute] int testId,
            [FromQuery] OptionsTest options)
            => Ok(_managerTest.GetTestForStudentById(testId, GetUserId(), options));

        [HttpGet("Current/Tests/{testId:int}/Data")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestData([FromRoute] int testId)
            => Ok(_managerTestData.GetTestDataForStudentByTestId(testId, GetUserId()));

        [HttpGet("Current/Tests/{testId:int}/Execution")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentTestExecution(
            [FromRoute] int testId,
            [FromQuery] OptionsTestExecution options)
            => Ok(_managerTestExecution.GetStudentTestExecution(testId, GetUserId(), options));

        [HttpGet("Current/Disciplines")]
        [Roles(UserRoles.Student)]
        public IActionResult GetStudentDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
            => Ok(_managerDiscipline.GetDisciplinesForStudent(GetUserId(), options, filter));
    }
}