using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source.Files;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Images")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerImage : Tamer
    {
        private readonly IManagerFileImage _managerFileImage;

        public TamerImage(IManagerFileImage managerFileImage)
        {
            _managerFileImage = managerFileImage;
        }

        [HttpPost("")]
        public IActionResult SaveImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
                return Ok(_managerFileImage.SaveFile(new File(file.FileName, stream)));
        }
    }
}