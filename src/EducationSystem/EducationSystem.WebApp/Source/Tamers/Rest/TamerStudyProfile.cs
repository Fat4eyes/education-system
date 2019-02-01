﻿using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/StudyProfiles")]
    public class TamerStudyProfile : TamerBase
    {
        protected IManagerStudyProfile ManagerStudyProfile { get; }

        public TamerStudyProfile(IManagerStudyProfile managerStudyProfile)
        {
            ManagerStudyProfile = managerStudyProfile;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(ManagerStudyProfile.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerStudyProfile.GetById(id));
        }
    }
}