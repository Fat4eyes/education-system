using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Validators;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Implementations.Validators.Files.Basics
{
    public abstract class ValidatorFile<TFile> : IValidator<TFile> where TFile : File
    {
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
            .ToUpperInvariant()
            .ToArray();

        public virtual Task ValidateAsync(TFile model)
        {
            if (model?.Stream == null || model.Stream.Length == 0)
                throw ExceptionHelper.CreatePublicException("Не указан файл.");

            if (model.Type == FileType.Any)
                throw ExceptionHelper.CreatePublicException("Указан неверный тип файла.");

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название файла.");

            if (AvailableExtensionsInUpperCase.Contains(Path.GetExtension(model.Name).ToUpperInvariant()) == false)
                throw ExceptionHelper.CreatePublicException(
                    $"Файл имеет неверное расширение. " +
                    $"Доступные расширения файла: {string.Join(", ", AvailableExtensions)}.");

            if (model.Stream.Length > MaxiFileSize * 1024 * 1024)
                throw ExceptionHelper.CreatePublicException(
                    $"Размер файла превышает допустимый размер. " +
                    $"Допустимый размер файла: {MaxiFileSize} MB.");

            return Task.CompletedTask;
        }
    }
}