using System.Collections.Generic;
using System.IO;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source.Files;
using File = EducationSystem.Models.Source.Files.File;

namespace EducationSystem.Helpers.Implementations.Source.Files
{
    public abstract class HelperFile : IHelperFile
    {
        /// <summary>
        /// Максимальный размер файла (в мегабайтах).
        /// </summary>
        protected abstract int MaxiFileSize { get; }

        /// <summary>
        /// Доступные расширения файла.
        /// </summary>
        protected abstract List<string> AvailableExtensions { get; }

        public virtual void ValidateFile(File file)
        {
            if (file?.Stream == null || file.Stream.Length == 0)
                throw ExceptionHelper.CreatePublicException("Не указан файл.");

            if (AvailableExtensions.Contains(Path.GetExtension(file.Name)) == false)
                throw ExceptionHelper.CreatePublicException(
                    $"Файл имеет неверный расширение. " +
                    $"Доступные расширения файла: {string.Join(", ", AvailableExtensions)}.");

            if (file.Stream.Length > MaxiFileSize * 1024 * 1024)
                throw ExceptionHelper.CreatePublicException(
                    $"Размер файла превышает допустимый размер. " +
                    $"Допустимый размер файла: {MaxiFileSize} MB.");
        }
    }
}