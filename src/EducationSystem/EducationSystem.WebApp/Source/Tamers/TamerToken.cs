using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces;
using EducationSystem.Models;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Token")]
    public class TamerToken : Tamer
    {
        private readonly ITokenGenerator _tokenGenerator;

        public TamerToken(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("Generate")]
        public async Task<IActionResult> Generate([FromBody] TokenRequest request)
        {
            return await Ok(() => _tokenGenerator.GenerateTokenAsync(request));
        }

        [HttpPost]
        [Route("Check")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public IActionResult Check() => Ok();
    }
}