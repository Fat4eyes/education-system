using System.Threading.Tasks;
using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Files;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Documents")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerDocument : Tamer
    {
        private readonly IManagerFileDocument _managerFileDocument;

        public TamerDocument(IManagerFileDocument managerFileDocument)
        {
            _managerFileDocument = managerFileDocument;
        }

        [HttpPost("")]
        public async Task<IActionResult> SaveDocument(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _managerFileDocument
                    .SaveFileAsync(new File(file.FileName, stream));

                return Ok(result);
            }
        }
    }
}