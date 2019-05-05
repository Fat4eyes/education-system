using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Users")]
    public class TamerUser : Tamer
    {
        private readonly IServiceUser _serviceUser;

        public TamerUser(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpGet("Current")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetCurrentUser()
        {
            return await Ok(() => _serviceUser.GetCurrentUserAsync());
        }
    }
}