using System.Threading.Tasks;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
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
        public async Task<IActionResult> Generate([FromBody] TokenRequest request)
        {
            return Ok(await _managerToken.GenerateTokenAsync(request));
        }

        [HttpPost]
        [Authorize]
        [Route("Check")]
        public IActionResult Check() => Ok();
    }
}