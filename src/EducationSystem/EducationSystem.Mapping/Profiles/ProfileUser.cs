using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileUser : Profile
    {
        public ProfileUser()
        {
            CreateMap<DatabaseUser, User>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)));
        }
    }
}