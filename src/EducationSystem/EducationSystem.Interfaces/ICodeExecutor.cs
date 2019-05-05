using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Code;

namespace EducationSystem.Interfaces
{
    public interface ICodeExecutor
    {
        Task<CodeExecutionResponse> ExecuteAsync(CodeExecutionRequest request);
    }
}