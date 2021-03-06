﻿using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileAnswer : Profile
    {
        public ProfileAnswer()
        {
            CreateMap<DatabaseAnswer, Answer>()
                .ForMember(d => d.Status, o => o.Ignore());

            CreateMap<Answer, DatabaseAnswer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());

            CreateMap<DatabaseAnswer, DatabaseAnswer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());
        }
    }
}