using EducationSystem.Constants;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files;
using EducationSystem.WebApp.Source.Tamers.Files.Basics;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
{
    [Route("api/Documents")]
    public class TamerDocument : TamerFile<Document>
    {
        public TamerDocument(IServiceFile<Document> serviceDocument)
            : base(serviceDocument)
        { }

        public override IActionResult GetExtensions()
        {
            return Ok(FileExtensions.AvailableDocumentExtensions);
        }
    }
}