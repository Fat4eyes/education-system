using System;
using System.Threading.Tasks;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;

namespace EducationSystem.Implementations.Code
{
    public sealed class CodeTesterApi : ICodeTesterApi
    {
        private readonly ICodeAnalysisApi _codeAnalysisApi;
        private readonly ICodeExecutionApi _codeExecutionApi;

        private static readonly Random Random = new Random();

        public CodeTesterApi(ICodeAnalysisApi codeAnalysisApi, ICodeExecutionApi codeExecutionApi)
        {
            _codeAnalysisApi = codeAnalysisApi;
            _codeExecutionApi = codeExecutionApi;
        }

        public async Task<CodeTestingResult> TestCode(TestingCode code)
        {
            var result = new CodeTestingResult
            {
                CodeAnalysisResult = await _codeAnalysisApi.AnalyzeCodeAsync(code),
                Score = (byte) Random.Next(1, 100 + 1)
            };

            if (result.CodeAnalysisResult.IsSuccessful)
                result.CodeExecutionResult = await _codeExecutionApi.ExecuteCodeAsync(code);

            return result;
        }
    }
}