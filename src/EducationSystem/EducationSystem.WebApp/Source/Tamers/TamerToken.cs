﻿using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
using EducationSystem.WebApp.Source.Tamers.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Token")]
    public class TamerToken : Tamer
    {
        protected IManagerToken ManagerToken { get; }

        public TamerToken(IManagerToken managerToken)
        {
            ManagerToken = managerToken;
        }

        [HttpPost]
        [Route("generate")]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            return Json(ManagerToken.GenerateToken(request));
        }

        [HttpPost]
        [Authorize]
        [Route("check")]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}