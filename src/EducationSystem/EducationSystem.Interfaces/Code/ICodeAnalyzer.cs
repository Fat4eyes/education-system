using System.Threading.Tasks;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Code
{
    public interface ICodeAnalyzer
    {
        Task<CodeAnalysisResult> AnalyzeAsync(Program program);
    }
}