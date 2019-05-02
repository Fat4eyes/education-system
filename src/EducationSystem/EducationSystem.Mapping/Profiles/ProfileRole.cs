using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileRole : Profile
    {
        public ProfileRole()
        {
            CreateMap<DatabaseRole, Role>();
        }
    }
}