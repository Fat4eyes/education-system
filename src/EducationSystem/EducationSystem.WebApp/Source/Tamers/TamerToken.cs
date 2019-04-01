using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
using EducationSystem.Models.Source;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Token")]
    public class TamerToken : Tamer
    {
        private readonly IManagerToken _managerToken;

        public TamerToken(IManagerToken managerToken)
        {
            _managerToken = managerToken;
        }

        [HttpPost]
        [Route("Generate")]
        public IActionResult Generate([FromBody] TokenRequest request) =>
            Ok(_managerToken.GenerateToken(request));

        [HttpPost]
        [Authorize]
        [Route("Check")]
        public IActionResult Check() => Ok();
    }
}