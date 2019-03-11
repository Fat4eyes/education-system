using EducationSystem.Constants.Source;
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

        [Transaction]
        [HttpPost("")]
        public IActionResult SaveDocument(IFormFile file) =>
            Ok(_managerFileDocument.SaveImage(new File(file.FileName, file.OpenReadStream())));
    }
}