using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
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
        public IActionResult Generate([FromBody] TokenRequest request) =>
            Ok(ManagerToken.GenerateToken(request));

        [HttpPost]
        [Authorize]
        [Route("check")]
        public IActionResult Check() => Ok();
    }
}