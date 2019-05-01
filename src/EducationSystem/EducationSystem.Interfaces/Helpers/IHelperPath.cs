using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperPath
    {
        string GetContentPath();

        Task<string> GetAbsoluteFilePathAsync(File file);

        Task<string> GetRelativeFilePathAsync(File file);

        string GetRelativeFilePath(DatabaseFile file);

        string GetRelativeFilePath(FileType type, Guid guid, string name);
    }
}