using System;
using System.IO;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Specifications.Files;
using Microsoft.AspNetCore.Hosting;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperPath : IHelperPath
    {
        private readonly IHostingEnvironment _environment;
        private readonly IRepository<DatabaseFile> _repositoryFile;

        public HelperPath(IHostingEnvironment environment, IRepository<DatabaseFile> repositoryFile)
        {
            _environment = environment;
            _repositoryFile = repositoryFile;
        }

        public string GetContentPath() => _environment.ContentRootPath;

        public async Task<string> GetAbsoluteFilePathAsync(File file)
            => Path.Combine(GetContentPath(), await GetRelativeFilePathAsync(file));

        public async Task<string> GetRelativeFilePathAsync(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            DatabaseFile model = null;

            if (file.Guid.HasValue)
                model = await _repositoryFile.FindFirstAsync(new FilesByGuid(file.Guid.Value));

            model = model ?? await _repositoryFile.FindFirstAsync(new FilesById(file.Id)) ??
                throw ExceptionHelper.NotFound<DatabaseFile>(file.Id);

            return GetRelativeFilePath(model);
        }

        public string GetRelativeFilePath(DatabaseFile file)
        {
            return GetRelativeFilePath(file.Type, new Guid(file.Guid), file.Name);
        }

        string IHelperPath.GetRelativeFilePath(FileType type, Guid guid, string name)
        {
            return GetRelativeFilePath(type, guid, name);
        }

        public static string GetRelativeFilePath(FileType type, Guid guid, string name)
        {
            return Path.Combine(Directories.Files, HelperFolder.GetFolderName(type), guid + Path.GetExtension(name));
        }
    }
}