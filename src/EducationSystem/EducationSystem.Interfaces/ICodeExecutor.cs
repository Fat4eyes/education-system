using System.Threading.Tasks;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces
{
    public interface ICodeExecutor
    {
        Task<CodeExecutionResult> ExecuteAsync(Program program);
    }
}