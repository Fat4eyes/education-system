﻿using System;
using System.IO;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
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
        private readonly IHelperFolder _helperFolder;
        private readonly IRepositoryFile _repositoryFile;

        public HelperPath(
            IHostingEnvironment environment,
            IHelperFolder helperFolder,
            IRepositoryFile repositoryFile)
        {
            _environment = environment;
            _helperFolder = helperFolder;
            _repositoryFile = repositoryFile;
        }

        public string GetContentPath() => _environment.ContentRootPath;

        public string GetAbsoluteFilePath(File file)
            => Path.Combine(GetContentPath(), GetRelativeFilePath(file));

        public string GetRelativeFilePath(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            DatabaseFile model = null;

            if (file.Guid.HasValue)
                model = _repositoryFile.GetByGuid(file.Guid.Value);

            model = model ?? _repositoryFile.GetById(file.Id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {file.Id}.",
                    $"Файл не найден.");

            return GetRelativeFilePath(model);
        }

        public string GetRelativeFilePath(DatabaseFile file)
        {
            var name = file.Guid + Path.GetExtension(file.Name);

            return Path.Combine(Directories.Files, _helperFolder.GetFolderName(file.Type), name);
        }
    }
}