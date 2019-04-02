using System.Threading.Tasks;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Models.Files;
using EducationSystem.WebApp.Source.Attributes;
using EducationSystem.WebApp.Source.Tamers.Files.Basics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
{
    [Route("api/Images")]
    public class TamerImage : TamerFile<Image>
    {
        public TamerImage(IManagerImage managerImage)
            : base(managerImage)
        { }

        [Authorize]
        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
                return Ok(await ManagerFile.UploadFile(new Image(file.FileName, stream)));
        }
    }
}