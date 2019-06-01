using AutoMapper;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Enums;
using EducationSystem.Mapping.Converts;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;
using ExternalCodeExecutionResult = CodeExecutionSystem.Contracts.Data.CodeExecutionResult;
using ExternalCodeAnalysisResult = CodeExecutionSystem.Contracts.Data.CodeAnalysisResult;
using CodeExecutionResult = EducationSystem.Models.Code.CodeExecutionResult;
using CodeAnalysisResult = EducationSystem.Models.Code.CodeAnalysisResult;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileCode : Profile
    {
        public ProfileCode()
        {
            // Запрос.

            CreateMap<Program, TestingCode>()
                .ForMember(d => d.Limits, o => o.MapFrom(s => s))
                .ForMember(d => d.ExecutionData, o => o.MapFrom(s => s.ProgramDatas))
                .ForMember(d => d.Language, o => o.ConvertUsing<LanguageConverter, LanguageType>(s => s.LanguageType.Value))
                .ForMember(d => d.Text, o => o.MapFrom(s => s.Source))
                .ForMember(d => d.Author, o => o.Ignore());

            CreateMap<Program, Limits>()
                .ForMember(d => d.MemoryLimitInBytes, o => o.MapFrom(s => s.MemoryLimit * 1024))
                .ForMember(d => d.TimeLimitInMs, o => o.MapFrom(s => s.TimeLimit * 1000));

            CreateMap<ProgramData, ExecutionData>()
                .ForMember(d => d.OutputData, o => o.MapFrom(s => s.ExpectedOutput))
                .ForMember(d => d.InputData, o => o.MapFrom(s => s.Input));

            // Ответ.

            CreateMap<ExternalCodeExecutionResult, CodeExecutionResult>()
                .ForMember(d => d.Errors, o => o.MapFrom(s => s.CompilationErrors));

            CreateMap<TestRunResult, CodeRunResult>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.ExecutionResult));

            CreateMap<ExternalCodeAnalysisResult, CodeAnalysisResult>()
                .ForMember(d => d.Success, o => o.MapFrom(s => s.IsSuccessful))
                .ForMember(d => d.Messages, o => o.MapFrom(s => s.AnalysisResults));

            CreateMap<AnalysisResult, CodeAnalysisMessage>()
                .ForMember(d => d.Text, o => o.MapFrom(s => s.Message))
                .ForMember(d => d.IsError, o => o.MapFrom(s => s.Level == Level.Error))
                .ForMember(d => d.IsWarning, o => o.MapFrom(s => s.Level == Level.Warning));

            CreateMap<CodeTestingResult, CodeRunningResult>();
        }
    }
}