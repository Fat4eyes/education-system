using System.Threading.Tasks;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Code
{
    public interface ICodeRunner
    {
        Task<CodeRunningResult> RunAsync(Program program);
    }
}