﻿using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileProgram : Profile
    {
        public ProfileProgram()
        {
            CreateMap<DatabaseProgram, Program>()
                .ForMember(d => d.Source, o => o.Ignore())
                .ForMember(d => d.CodeRunningResult, o => o.Ignore());

            CreateMap<Program, Program>()
                .ForMember(d => d.Source, o => o.Ignore())
                .ForMember(d => d.CodeRunningResult, o => o.Ignore());

            CreateMap<Program, DatabaseProgram>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());

            CreateMap<DatabaseProgram, DatabaseProgram>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore())
                .ForMember(d => d.ProgramDatas, o => o.Ignore());
        }
    }
}