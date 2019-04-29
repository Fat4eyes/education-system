using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Files.Basics;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Files.Basics.File;
using SystemFile = System.IO.File;

namespace EducationSystem.Implementations.Managers.Files.Basics
{
    public abstract class ManagerFile<TFile> : Manager<ManagerFile<TFile>>, IManagerFile<TFile>
        where TFile : File
    {
        private readonly IHelperPath _helperPath;
        private readonly IHelperFile _helperFile;
        private readonly IHelperFolder _helperFolder;
        private readonly IValidator<TFile> _validatorFile;
        private readonly IRepositoryFile _repositoryFile;

        protected ManagerFile(
            IMapper mapper,
            ILogger<ManagerFile<TFile>> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<TFile> validatorFile,
            IRepositoryFile repositoryFile)
            : base(mapper, logger)
        {
            _helperPath = helperPath;
            _helperFile = helperFile;
            _helperFolder = helperFolder;
            _validatorFile = validatorFile;
            _repositoryFile = repositoryFile;
        }

        public async Task DeleteFile(int id)
        {
            var model = await _repositoryFile.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл для удаления не найден. Идентификатор файла: {id}.",
                    $"Файл для удаления не найден.");

            var file = Mapper.Map<TFile>(model);

            await _repositoryFile.RemoveAsync(model);

            if (await _helperFile.IsFileExists(file) == false)
                return;

            var path = await _helperPath.GetAbsoluteFilePath(file);

            if (SystemFile.Exists(path))
                SystemFile.Delete(path);

            await _repositoryFile.SaveChangesAsync();
        }

        public async Task<TFile> GetFile(int id)
        {
            var model = _repositoryFile.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {id}.",
                    $"Файл не найден.");

            var file = Mapper.Map<TFile>(model);

            if (await _helperFile.IsFileExists(file) == false)
                throw ExceptionHelper.CreateNotFoundException(
                    $"Файл не найден. Идентификатор файла: {id}.",
                    $"Файл не найден.");

            file.Path = await _helperPath.GetRelativeFilePath(file);

            file.Path = file.Path.Replace("\\", "/");

            return file;
        }

        public virtual async Task<TFile> CreateFile(TFile file)
        {
            await _validatorFile.Validate(file);

            var guid = Guid.NewGuid();
            var name = guid + Path.GetExtension(file.Name);

            var path = Path.Combine(_helperPath.GetContentPath(), Directories.Files);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, _helperFolder.GetFolderName(file.Type));

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            path = Path
                .Combine(Directories.Files, _helperFolder.GetFolderName(file.Type), name)
                .Replace("\\", "/");

            var model = Mapper.Map<DatabaseFile>(file);

            model.Guid = guid.ToString();
            model.Type = file.Type;
            model.Name = file.Name;

            await _repositoryFile.AddAsync(model, true);

            var result = Mapper.Map<TFile>(model);

            result.Path = path;

            return result;
        }
    }
}