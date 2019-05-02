using System;
using System.IO;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperPath : IHelperPath
    {
        private readonly IHostingEnvironment _environment;
        private readonly IRepositoryFile _repositoryFile;

        public HelperPath(IHostingEnvironment environment, IRepositoryFile repositoryFile)
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
                model = await _repositoryFile.GetFileAsync(file.Guid.Value);

            model = model ?? await _repositoryFile.GetByIdAsync(file.Id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {file.Id}.",
                    $"Файл не найден.");

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