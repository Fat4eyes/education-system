using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Models;

namespace EducationSystem.Mapping.Profiles
{
    public class ProfileMaterialAnchor : Profile
    {
        public ProfileMaterialAnchor()
        {
            CreateMap<DatabaseMaterialAnchor, MaterialAnchor>();

            CreateMap<MaterialAnchor, DatabaseMaterialAnchor>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.MaterialId, o => o.Ignore());
        }
    }
}