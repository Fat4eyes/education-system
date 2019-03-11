using System.IO;
using System.Linq;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source.Files;
using Microsoft.AspNetCore.Hosting;
using File = EducationSystem.Models.Source.Files.File;
using SystemFile = System.IO.File;

namespace EducationSystem.Helpers.Implementations.Source.Files
{
    public abstract class HelperFile : IHelperFile
    {
        private readonly IHostingEnvironment _environment;

        protected HelperFile(IHostingEnvironment environment)
        {
            _environment = environment;
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

        public bool FileExsists(File file)
        {
            var path = Path.Combine(
                _environment.ContentRootPath,
                file.Path.Replace("/", "\\"));

            return SystemFile.Exists(path);
        }
    }
}