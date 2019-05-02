using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Models.Files;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Rest;

namespace EducationSystem.Mapping
{
    public static class MappingConfigurator
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<DatabaseUser, User>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active == 1))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(x => x.Role)));

            expression.CreateMap<DatabaseDiscipline, Discipline>()
                .ForMember(d => d.Tests, o => o.Ignore())
                .ForMember(d => d.Themes, o => o.Ignore())
                .ForMember(d => d.Lecturers, o => o.Ignore());

            expression.CreateMap<DatabaseGroup, Group>();

            expression.CreateMap<DatabaseTest, Test>()
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive == 1))
                .ForMember(d => d.Attempts, o => o.Ignore())
                .ForMember(d => d.TotalTime, o => o.Ignore())
                .ForMember(d => d.Themes, o => o.Ignore());

            expression.CreateMap<Test, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive ? 1 : 0))
                .ForMember(d => d.TestThemes, o => o.MapFrom(s => s.Themes));

            expression.CreateMap<DatabaseTest, DatabaseTest>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.TestThemes, o => o.Ignore());

            expression.CreateMap<Theme, DatabaseTestTheme>()
                .ForMember(d => d.ThemeId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.TestId, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Test, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            expression.CreateMap<DatabaseTheme, Theme>()
                .ForMember(d => d.Questions, o => o.Ignore());

            expression.CreateMap<Theme, DatabaseTheme>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.ThemeTests, o => o.Ignore())
                .ForMember(d => d.Questions, o => o.Ignore());

            expression.CreateMap<DatabaseTheme, DatabaseTheme>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.Discipline, o => o.Ignore())
                .ForMember(d => d.ThemeTests, o => o.Ignore())
                .ForMember(d => d.Questions, o => o.Ignore());

            expression.CreateMap<DatabaseQuestion, Question>()
                .ForMember(d => d.Answers, o => o.Ignore());

            expression.CreateMap<Question, DatabaseQuestion>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Image, o => o.Ignore())
                .ForMember(d => d.ImageId, o => o.MapFrom(d => d.Image.Id))
                .ForMember(d => d.Answers, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.QuestionStudents, o => o.Ignore())
                .ForMember(d => d.MaterialId, o => o.MapFrom(d => d.Material.Id));

            expression.CreateMap<DatabaseQuestion, DatabaseQuestion>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Theme, o => o.Ignore())
                .ForMember(d => d.Answers, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.Order, o => o.Ignore())
                .ForMember(d => d.QuestionStudents, o => o.Ignore());

            expression.CreateMap<DatabaseAnswer, Answer>()
                .ForMember(d => d.IsRight, o => o.MapFrom(s => s.IsRight == 1));

            expression.CreateMap<Answer, DatabaseAnswer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());

            expression.CreateMap<DatabaseAnswer, DatabaseAnswer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());

            expression.CreateMap<DatabaseProgram, Program>()
                .ForMember(d => d.ProgramDatas, o => o.Ignore());

            expression.CreateMap<Program, DatabaseProgram>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore());

            expression.CreateMap<DatabaseProgram, DatabaseProgram>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Question, o => o.Ignore())
                .ForMember(d => d.QuestionId, o => o.Ignore())
                .ForMember(d => d.ProgramDatas, o => o.Ignore());

            expression.CreateMap<DatabaseProgramData, ProgramData>();

            expression.CreateMap<ProgramData, DatabaseProgramData>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.ProgramId, o => o.Ignore());

            expression.CreateMap<DatabaseRole, Role>();

            expression.CreateMap<DatabaseMaterial, Material>()
                .ForMember(d => d.Files, o => o.MapFrom(s => s.Files.Select(x => x.File)));

            expression.CreateMap<Material, DatabaseMaterial>()
                .ForMember(d => d.Id, o => o.Ignore());

            expression.CreateMap<DatabaseMaterial, DatabaseMaterial>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Files, o => o.Ignore());

            expression.CreateMap<DatabaseFile, Image>()
                .ForMember(d => d.Path, o => o.Ignore())
                .ForMember(d => d.Stream, o => o.Ignore())
                .AfterMap((s, d) => d.Path = GetDocumentPath(d));

            expression.CreateMap<DatabaseFile, Document>()
                .ForMember(d => d.Path, o => o.Ignore())
                .ForMember(d => d.Stream, o => o.Ignore())
                .AfterMap((s, d) => d.Path = GetDocumentPath(d));

            expression.CreateMap<Document, DatabaseMaterialFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.FileId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.MaterialId, o => o.Ignore())
                .ForMember(d => d.Material, o => o.Ignore())
                .ForMember(d => d.File, o => o.Ignore());

            expression.CreateMap<Document, DatabaseFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.Guid, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore());

            expression.CreateMap<Image, DatabaseFile>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.Guid, o => o.Ignore())
                .ForMember(d => d.Owner, o => o.Ignore())
                .ForMember(d => d.OwnerId, o => o.Ignore());

            expression.AllowNullCollections = true;
            expression.ForAllMaps(Configure);
        }

        private static void Configure(TypeMap x, IMappingExpression expression)
            => expression.PreserveReferences();

        private static string GetDocumentPath(File file)
        {
            if (file.Guid.HasValue == false)
                return null;

            return HelperPath
                .GetRelativeFilePath(file.Type, file.Guid.Value, file.Name)
                .Replace("\\", "/");
        }
    }
}