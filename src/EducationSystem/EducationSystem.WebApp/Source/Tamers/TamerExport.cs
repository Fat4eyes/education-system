using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Export;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Exports")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerExport : Tamer
    {
        private readonly IManagerExportQuestion _managerExportQuestion;

        public TamerExport(IManagerExportQuestion managerExportQuestion)
        {
            _managerExportQuestion = managerExportQuestion;
        }

        [HttpGet("Themes/{themeId:int}/Questions")]
        public IActionResult ExportThemeQuestions([FromRoute] int themeId) =>
            File(_managerExportQuestion.ExportThemeQuestions(themeId));
    }
}