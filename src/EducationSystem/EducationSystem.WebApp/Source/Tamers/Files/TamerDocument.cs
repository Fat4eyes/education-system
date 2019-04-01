using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Models;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
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

        [HttpGet("{documentId:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int documentId)
            => Ok(await _managerFileDocument.GetFileById(documentId));

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> AddDocument(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _managerFileDocument
                    .AddFileAsync(new File(file.FileName, stream));

                return Ok(result);
            }
        }

        [Transaction]
        [HttpDelete("{documentId:int}")]
        public IActionResult DeleteDocument([FromRoute] int documentId)
            => Ok(async () => await _managerFileDocument.DeleteFileByIdAsync(documentId));
    }
}