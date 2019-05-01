using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers.Files.Basics;
using EducationSystem.Models.Files.Basics;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files.Basics
{
    public abstract class TamerFile<TFile> : Tamer where TFile : File, new()
    {
        protected IManagerFile<TFile> ManagerFile { get; }

        protected TamerFile(IManagerFile<TFile> managerFile)
        {
            ManagerFile = managerFile;
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFile([FromRoute] int id)
        {
            return Ok(await ManagerFile.GetFileAsync(id));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteFile([FromRoute] int id)
        {
            await ManagerFile.DeleteFileAsync(id);

            return Ok();
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateFile(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var model = new TFile
                {
                    Name = file.FileName,
                    Stream = stream
                };

                return Ok(await ManagerFile.CreateFileAsync(model));
            }
        }

        [HttpGet("Extensions")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public abstract IActionResult GetExtensions();
    }
}