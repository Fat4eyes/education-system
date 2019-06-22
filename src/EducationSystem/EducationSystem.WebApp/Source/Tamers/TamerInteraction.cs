using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("interaction")]
    public class TamerInteraction : Controller
    {
        private readonly ITokenGenerator _tokenGenerator;

        private static string TestingSystemAddress = "http://www.web-test.ru";

        private const string TokenCookieName = "Token";

        public TamerInteraction(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;

            TestingSystemAddress = configuration[nameof(TestingSystemAddress)];
        }

        [Route("login")]
        public async Task<IActionResult> LogIn(LogInRequest request)
        {
            var response = await _tokenGenerator.GenerateTokenAsync(request);

            Response.Cookies.Append(TokenCookieName, response.Token);

            var url = string.IsNullOrWhiteSpace(request.Address)
                ? TestingSystemAddress
                : request.Address;

            return Redirect(url);
        }

        [Route("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete(TokenCookieName);

            return Redirect(TestingSystemAddress + "/logout");
        }
    }
}