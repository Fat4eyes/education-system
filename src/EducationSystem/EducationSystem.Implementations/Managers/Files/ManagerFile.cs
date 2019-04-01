using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Source.Files.File;
using SystemFile = System.IO.File;

namespace EducationSystem.Implementations.Managers.Files
{
    public abstract class ManagerFile : Manager<ManagerFile>, IManagerFile
    {
        protected abstract FileType FileType { get; }

        private readonly IHelperFile _helperFile;
        private readonly IHostingEnvironment _environment;
        private readonly IRepositoryFile _repositoryFile;

        protected ManagerFile(
            IMapper mapper,
            ILogger<ManagerFile> logger,
            IHelperFile helperFile,
            IHostingEnvironment environment,
            IRepositoryFile repositoryFile)
            : base(mapper, logger)
        {
            _helperFile = helperFile;
            _environment = environment;
            _repositoryFile = repositoryFile;
        }

        public virtual async Task<File> AddFileAsync(File file)
        {
            _helperFile.ValidateFile(file);

            var guid = Guid.NewGuid();
            var name = guid + Path.GetExtension(file.Name);

            var path = Path.Combine(_environment.ContentRootPath, Directories.Files);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, _helperFile.GetFolderName(FileType));

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            path = Path
                .Combine(Directories.Files, _helperFile.GetFolderName(FileType), name)
                .Replace("\\", "/");

            var model = Mapper.Map<DatabaseFile>(file);

            model.Guid = guid.ToString();
            model.Type = FileType;
            model.Name = file.Name;

            await _repositoryFile.AddAsync(model, true);

            var result = Mapper.Map<File>(model);

            result.Path = path;

            return result;
        }

        public Task<File> GetFileById(int id)
        {
            var model = _repositoryFile.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {id}.",
                    $"Файл не найден.");

            var file = Mapper.Map<File>(model);

            if (_helperFile.FileExists(file) == false)
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {id}.",
                    $"Файл не найден.");

            file.Path = _helperFile
                .GetFilePath(file)
                .Replace("\\", "/");

            return Task.FromResult(file);
        }

        public async Task DeleteFileByIdAsync(int id)
        {
            var model = _repositoryFile.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл для удаления не найден. Идентификатор файла: {id}.",
                    $"Файл для удаления не найден.");

            var file = Mapper.Map<File>(model);

            await _repositoryFile.RemoveAsync(model);

            if (_helperFile.FileExists(file) == false)
                return;

            var path = Path.Combine(_environment.ContentRootPath, _helperFile.GetFilePath(file));

            if (SystemFile.Exists(path))
                SystemFile.Delete(path);

            await _repositoryFile.SaveChangesAsync();
        }
    }
}