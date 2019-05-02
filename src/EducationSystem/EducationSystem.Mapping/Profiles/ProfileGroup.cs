using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileGroup : Profile
    {
        public ProfileGroup()
        {
            CreateMap<DatabaseGroup, Group>();
        }
    }
}