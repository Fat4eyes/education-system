using System.Threading.Tasks;
using EducationSystem.Interfaces.Managers.Files.Basics;
using EducationSystem.Models.Files.Basics;
using EducationSystem.WebApp.Source.Attributes;
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

        [HttpGet("{documentId:int}")]
        public async Task<IActionResult> GetImage([FromRoute] int documentId)
            => Ok(await ManagerFile.GetFileById(documentId));

        [HttpGet("Extensions")]
        public IActionResult GetAvailableExtensions()
            => Ok(ManagerFile.GetAvailableExtensions());

        [Transaction]
        [HttpDelete("{documentId:int}")]
        public IActionResult DeleteDocument([FromRoute] int documentId)
            => Ok(async () => await ManagerFile.DeleteFileByIdAsync(documentId));
    }
}