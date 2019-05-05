using System;
using System.Linq;
using System.Threading.Tasks;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;

namespace EducationSystem.Implementations
{
    public sealed class CodeExecutionApi : ICodeExecutionApi
    {
        private static readonly Random Random = new Random();

        public Task<CodeExecutionResult> ExecuteCodeAsync(TestingCode code)
        {
            var results = new[]
            {
                GetErrorResult(),
                GetSuccessResult()
            };

            return Task.FromResult(results[Random.Next(results.Length)]);
        }

        private static CodeExecutionResult GetSuccessResult()
        {
            return new CodeExecutionResult
            {
                Results = new[]
                {
                    CreateSuccessResult(),
                    CreateSuccessResult(),
                    CreateSuccessResult(),
                    CreateSuccessResult(),
                    CreateSuccessResult()
                }
            };
        }

        private static CodeExecutionResult GetErrorResult()
        {
            return new CodeExecutionResult
            {
                CompilationErrors = new[]
                {
                    GetRandomString(25),
                    GetRandomString(50),
                    GetRandomString(75),
                    GetRandomString(100)
                },
                Results = new[]
                {
                    CreateSuccessResult(),
                    CreateSuccessResult(),
                    CreateResult(ExecutionResult.KilledByMemoryLimit),
                    CreateResult(ExecutionResult.KilledByTimeout)
                }
            };
        }

        private static TestRunResult CreateSuccessResult()
        {
            return new TestRunResult
            {
                ExecutionResult = ExecutionResult.Success,
                ExpectedOutput = nameof(TestRunResult.ExpectedOutput),
                UserOutput = nameof(TestRunResult.ExpectedOutput)
            };
        }

        private static TestRunResult CreateResult(ExecutionResult result)
        {
            return new TestRunResult
            {
                ExecutionResult = result,
                ExpectedOutput = GetRandomString(25),
                UserOutput = GetRandomString(25)
            };
        }

        private static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray());
        }
    }
}