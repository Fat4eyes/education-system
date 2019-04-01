using System;
using System.IO;
using System.Linq;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using File = EducationSystem.Models.Source.Files.File;
using SystemFile = System.IO.File;

namespace EducationSystem.Implementations.Helpers.Files
{
    public abstract class HelperFile : IHelperFile
    {
        private readonly IHostingEnvironment _environment;
        private readonly IRepositoryFile _repositoryFile;

        protected HelperFile(IHostingEnvironment environment, IRepositoryFile repositoryFile)
        {
            _environment = environment;
            _repositoryFile = repositoryFile;
        }

        /// <summary>
        /// Максимальный размер файла (в мегабайтах).
        /// </summary>
        protected abstract int MaxiFileSize { get; }

        /// <summary>
        /// Доступные расширения файла.
        /// </summary>
        protected abstract string[] AvailableExtensions { get; }

        /// <summary>
        /// Доступные расширения файла (в верхнем регистре).
        /// </summary>
        protected string[] AvailableExtensionsInUpperCase => AvailableExtensions
            .Select(x => x.ToUpper())
            .ToArray();

        public virtual void ValidateFile(File file)
        {
            if (file?.Stream == null || file.Stream.Length == 0)
                throw ExceptionHelper.CreatePublicException("Не указан файл.");

            if (string.IsNullOrWhiteSpace(file.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название файла.");

            if (AvailableExtensionsInUpperCase.Contains(Path.GetExtension(file.Name).ToUpper()) == false)
                throw ExceptionHelper.CreatePublicException(
                    $"Файл имеет неверное расширение. " +
                    $"Доступные расширения файла: {string.Join(", ", AvailableExtensions)}.");

            if (file.Stream.Length > MaxiFileSize * 1024 * 1024)
                throw ExceptionHelper.CreatePublicException(
                    $"Размер файла превышает допустимый размер. " +
                    $"Допустимый размер файла: {MaxiFileSize} MB.");
        }

        public bool FileExists(int id) => FileExists(new File(id));

        public bool FileExists(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(file.Path))
                return SystemFile.Exists(Path.Combine(_environment.ContentRootPath, GetFilePath(file)));

            var path = Path.Combine(
                _environment.ContentRootPath,
                file.Path.Replace("/", "\\"));

            return SystemFile.Exists(path);
        }

        public string GetFilePath(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            DatabaseFile model = null;

            if (file.Guid.HasValue)
                model =_repositoryFile.GetByGuid(file.Guid.Value);

            model = model ?? _repositoryFile.GetById(file.Id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {file.Id}.",
                    $"Файл не найден.");

            var name = model.Guid + Path.GetExtension(model.Name);

            return Path.Combine(Directories.Files, GetFolderName(model.Type), name);
        }

        public string GetFolderName(FileType type)
        {
            switch (type)
            {
                case FileType.Image:
                    return Directories.Images;
                case FileType.Document:
                    return Directories.Documents;
                default:
                    return Directories.Files;
            }
        }
    }
}