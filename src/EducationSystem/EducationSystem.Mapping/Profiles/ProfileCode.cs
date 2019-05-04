﻿using AutoMapper;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Enums;
using EducationSystem.Mapping.Converts;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileCode : Profile
    {
        public ProfileCode()
        {
            // Запрос.

            CreateMap<CodeExecutionRequest, TestingCode>()
                .ForMember(d => d.Limits, o => o.MapFrom(s => s.Program))
                .ForMember(d => d.Language, o => o.ConvertUsing<LanguageConverter, LanguageType>(s => s.Program.LanguageType.Value))
                .ForMember(d => d.Text, o => o.MapFrom(s => s.Source))
                .ForMember(d => d.Author, o => o.Ignore());

            CreateMap<Program, Limits>()
                .ForMember(d => d.MemoryLimitInBytes, o => o.MapFrom(s => s.MemoryLimit * 1024))
                .ForMember(d => d.TimeLimitInMs, o => o.MapFrom(s => s.TimeLimit * 1000));

            CreateMap<ProgramData, ExecutionData>()
                .ForMember(d => d.OutputData, o => o.MapFrom(s => s.ExpectedOutput))
                .ForMember(d => d.InputData, o => o.MapFrom(s => s.Input));

            // Ответ.

            CreateMap<CodeExecutionResult, CodeExecutionResponse>()
                .ForMember(d => d.Errors, o => o.MapFrom(s => s.CompilationErrors));

            CreateMap<TestRunResult, CodeRunResult>()
                .ForMember(d => d.Stutus, o => o.MapFrom(s => s.ExecutionResult));
        }
    }
}