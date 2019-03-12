using EducationSystem.Models.Source.Exports.Options;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Managers.Interfaces.Source.Export
{
    public interface IManagerExportQuestion : IManagerExport
    {
        File ExportThemeQuestions(int themeId, ExportOptions options);
    }
}