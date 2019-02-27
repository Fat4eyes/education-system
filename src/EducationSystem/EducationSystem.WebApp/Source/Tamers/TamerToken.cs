using EducationSystem.Managers.Interfaces.Source;
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
        [Route("generate")]
        public IActionResult Generate([FromBody] TokenRequest request) =>
            Ok(_managerToken.GenerateToken(request));

        [HttpPost]
        [Authorize]
        [Route("check")]
        public IActionResult Check() => Ok();
    }
}