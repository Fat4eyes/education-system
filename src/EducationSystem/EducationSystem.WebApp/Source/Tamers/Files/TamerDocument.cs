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
    [Route("api/Documents")]
    public class TamerDocument : TamerFile<Document>
    {
        public TamerDocument(IManagerDocument managerDocument)
            : base(managerDocument)
        { }

        [Authorize]
        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
                return Ok(await ManagerFile.UploadFile(new Document(file.FileName, stream)));
        }
    }
}