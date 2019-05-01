using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Models;
using Microsoft.AspNetCore.Authorization;
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
            return Ok(await _tokenGenerator.GenerateAsync(request));
        }

        [HttpPost]
        [Authorize]
        [Route("Check")]
        public IActionResult Check() => Ok();
    }
}