using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Models.Files;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
{
    [Route("api/Documents")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerDocument : Tamer
    {
        private readonly IManagerDocument _managerDocument;

        public TamerDocument(IManagerDocument managerDocument)
        {
            _managerDocument = managerDocument;
        }

        [HttpGet("{documentId:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int documentId)
            => Ok(await _managerDocument.GetFileById(documentId));

        [HttpGet("Extensions")]
        public IActionResult GetAvailableExtensions()
            => Ok(_managerDocument.GetAvailableExtensions());

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _managerDocument
                    .UploadFile(new Document(file.FileName, stream));

                return Ok(result);
            }
        }

        [Transaction]
        [HttpDelete("{documentId:int}")]
        public IActionResult DeleteDocument([FromRoute] int documentId)
            => Ok(async () => await _managerDocument.DeleteFileByIdAsync(documentId));
    }
}