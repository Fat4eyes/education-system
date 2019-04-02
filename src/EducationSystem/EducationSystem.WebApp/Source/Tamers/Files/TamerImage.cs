using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Models.Files;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
{
    [Route("api/Images")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerImage : Tamer
    {
        private readonly IManagerImage _managerImage;

        public TamerImage(IManagerImage managerImage)
        {
            _managerImage = managerImage;
        }

        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int imageId)
            => Ok(await _managerImage.GetFileById(imageId));

        [HttpGet("Extensions")]
        public IActionResult GetAvailableExtensions()
            => Ok(_managerImage.GetAvailableExtensions());

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _managerImage
                    .UploadFile(new Image(file.FileName, stream));

                return Ok(result);
            }
        }

        [Transaction]
        [HttpDelete("{imageId:int}")]
        public IActionResult DeleteDocument([FromRoute] int imageId)
            => Ok(async () => await _managerImage.DeleteFileByIdAsync(imageId));
    }
}