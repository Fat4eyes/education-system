using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileMaterial : Profile
    {
        public ProfileMaterial()
        {
            CreateMap<DatabaseMaterial, Material>()
                .ForMember(d => d.Files, o => o.MapFrom(s => s.Files.Select(x => x.File)));

            CreateMap<Material, DatabaseMaterial>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore());

            CreateMap<DatabaseMaterial, DatabaseMaterial>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Files, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore());
        }
    }
}