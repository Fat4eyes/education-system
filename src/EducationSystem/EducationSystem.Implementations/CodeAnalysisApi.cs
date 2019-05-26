using System;
using System.Linq;
using System.Threading.Tasks;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;

namespace EducationSystem.Implementations
{
    public sealed class CodeAnalysisApi : ICodeAnalysisApi
    {
        private static readonly Random Random = new Random();

        public Task<CodeAnalysisResult> AnalyzeCodeAsync(TestingCode code)
        {
            return Task.FromResult(GetCodeAnalysisResult());
        }

        private static CodeAnalysisResult GetCodeAnalysisResult()
        {
            return new CodeAnalysisResult
            {
                IsSuccessful = true,
                AnalysisResults = new[]
                {
                    CreateAnalysisResult(1, 5, Level.Error),
                    CreateAnalysisResult(2, 10, Level.Warning),
                    CreateAnalysisResult(13, 8, Level.Warning),
                    CreateAnalysisResult(15, 15, Level.Verbose)
                }
            };
        }

        private static AnalysisResult CreateAnalysisResult(int line, int column, Level level)
        {
            return new AnalysisResult
            {
                Line = line,
                Column = column,
                Level = level,
                Message = GetRandomString(Random.Next(50, 100 + 1))
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