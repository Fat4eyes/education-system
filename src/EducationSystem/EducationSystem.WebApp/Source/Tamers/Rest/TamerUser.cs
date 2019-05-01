using System.Threading.Tasks;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Users")]
    public class TamerUser : Tamer
    {
        private readonly IManagerUser _managerUser;

        public TamerUser(IManagerUser managerUser)
        {
            _managerUser = managerUser;
        }

        [Authorize]
        [HttpGet("Current")]
        public async Task<IActionResult> GetUser([FromQuery] OptionsUser options)
        {
            return Ok(await _managerUser.GetUserAsync(GetUserId(), options));
        }
    }
}