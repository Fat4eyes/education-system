using EducationSystem.Constants;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files;
using EducationSystem.WebApp.Source.Tamers.Files.Basics;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files
{
    [Route("api/Images")]
    public class TamerImage : TamerFile<Image>
    {
        public TamerImage(IServiceFile<Image> serviceImage)
            : base(serviceImage)
        { }

        public override IActionResult GetExtensions()
        {
            return Ok(FileExtensions.AvailableImageExtensions);
        }
    }
}