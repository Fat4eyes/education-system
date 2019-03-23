using System.Threading.Tasks;
using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Files;
using EducationSystem.Models.Source.Files;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
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

        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int imageId)
            => Ok(await _managerFileImage.GetFileById(imageId));

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _managerFileImage
                    .AddFileAsync(new File(file.FileName, stream));

                return Ok(result);
            }
        }

        [Transaction]
        [HttpDelete("{imageId:int}")]
        public IActionResult DeleteDocument([FromRoute] int imageId)
            => Ok(async () => await _managerFileImage.DeleteFileByIdAsync(imageId));
    }
}