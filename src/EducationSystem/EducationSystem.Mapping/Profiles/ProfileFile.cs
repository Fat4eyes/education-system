using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Models.Files;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Mapping.Profiles
{
    public sealed class ProfileFile : Profile
    {
        public ProfileFile()
        {
            CreateMap<DatabaseFile, Image>()
                .ForMember(d => d.Path, o => o.Ignore())
                .ForMember(d => d.Stream, o => o.Ignore())
                .AfterMap((s, d) => d.Path = GetDocumentPath(d));

            CreateMap<DatabaseFile, Document>()
                .ForMember(d => d.Path, o => o.Ignore())
                .ForMember(d => d.Stream, o => o.Ignore())
                .AfterMap((s, d) => d.Path = GetDocumentPath(d));

            CreateMap<Document, DatabaseFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.Guid, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore());

            CreateMap<Image, DatabaseFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.Guid, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore());

            CreateMap<Document, DatabaseMaterialFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.FileId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.MaterialId, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.File, o => o.Ignore());
        }

        private static string GetDocumentPath(File file)
        {
            if (file.Guid.HasValue == false)
                return null;

            return PathHelper
                .GetRelativeFilePath(file.Type, file.Guid.Value, file.Name)
                .Replace("\\", "/");
        }
    }
}