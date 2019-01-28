﻿using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Users")]
    public class TamerUser : Controller
    {
        protected IManagerUser ManagerUser { get; }

        public TamerUser(IManagerUser managerUser)
        {
            ManagerUser = managerUser;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Json(ManagerUser.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Json(ManagerUser.GetById(id));
        }
    }
}